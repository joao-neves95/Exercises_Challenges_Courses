; By: Joao Neves (SHIVAYL)
;
; Find and replace.
; Assembly 8086
;

#make_COM#
org 100h
jmp Main

.Data
  getInputInfo dw ' Insira uma frase: ' , 13 , 10 , '$'
  getToReplaceInfo dw ' Insira a palavra a ser substituida: ' , 13 , 10 , '$'
  getReplacerInfo dw ' Insira a palavra substituidora: ' , 13 , 10 , '$'
  outputInfo dw ' A frase tratada e: ' , 13 , 10 , '$'
  newLine db 13 , 10 , '$'
  ; VARIABLES
  inputStr db 80 dup(?)
  inputToReplaceStr db 80 dup(?)
  inputReplacerStr db 80 dup(?)
  outputStr db 80 dup(?)
  originalSI dw 2

.Code
  Main:
    call GetInputStr
    call GetToReplaceStr
    call GetReplacerStr
    call FindAndReplace
    call ShowOutputStr
    ; Fall to End.
  
  End:
    mov ah, 04CH
    INT 21H
  
  ; -------- SUBROUTINES --------- ;
  
  PrintNewLine PROC near
    lea dx, newLine
    mov ah, 9
    INT 21H
    ret
  PrintNewLine ENDP
  

  GetInputStr PROC near
    lea dx, getInputInfo
    mov ah, 9
    INT 21H
    
    lea di, inputStr
    
    _GetInputStr:
      mov ah, 1
      INT 21H
      
      mov [di], al
      
      cmp al, 13 ; Check if it's the end of the input.
        jz GetInputStr_END
      
      inc di
      jmp _GetInputStr
      
  GetInputStr_END:
    ret        
  GetInputStr ENDP
  
  
  GetToReplaceStr PROC near
    mov [di], '$'
    call PrintNewLine
    
    lea dx, getToReplaceInfo
    mov ah, 9
    INT 21H
    
    lea di, inputToReplaceStr
    
    _GetToReplaceStr:
      mov ah, 1
      INT 21H
      
      mov [di], al
      
      cmp al, 13 ; Check if it's the end of the input.
        jz GetToReplaceStr_END
        
      inc di
      jmp _GetToReplaceStr
  
  GetToReplaceStr_END:
    ret
  GetToReplaceStr ENDP
  
  
  GetReplacerStr PROC near
    mov [di], '$'
    call PrintNewLine
    
    lea dx, getReplacerInfo
    mov ah,9
    int 21H
    
    lea di, inputReplacerStr
    
    _GetReplacerStr:
      mov ah, 1
      INT 21H
      
      mov [di], al
      
      cmp al, 13 ; Check if it's the end of the input.
        jz GetReplacerStr_END
        
      inc di
      jmp _GetReplacerStr
  
  GetReplacerStr_END:
    ret
  GetReplacerStr ENDP
  
  
  FindAndReplace PROC near
    mov [di], '$'
    lea si, inputStr
    mov originalSI, si
    lea di, outputStr
    lea bx, inputToReplaceStr
    
    _Find:
      cmp [bx], '$' ; String to replace found (end of string to replace).
        jz _Replace
        
      mov al, [bx]
      cmp [si], al ; Check if the current chars are equal.
        jz _Find_INC
 
      cmp [si], '$' ; End of input.
        jz _FindAndReplace_END      
      
      ; String to replace not found.
      mov si, originalSI ; Return back.
      mov al, [si]
      mov [di], al ; Add char normally to the output.
      inc si
      mov originalSI, si
      inc di
      lea bx, inputToReplaceStr ; Return to the beggining of inputToReplaceStr.
      jmp _Find      
        
    _Find_INC:
      inc si
      inc bx
      jmp _Find      
       
    _Replace: ; The output builder
      mov originalSI, si ; Move SI forward.
      lea bx, inputReplacerStr
      ; Fall to _Replace_LOOP
      
      _Replace_LOOP:
        mov al, [bx]
        mov [di], al
        
        cmp al, '$' ; Continue until the end of string.
          jz _Replace_END
        
        inc bx
        inc di
        jmp _Replace_LOOP
      
      _Replace_END:
        lea bx, inputToReplaceStr ; Return to the beggining of inputReplacerStr.
        jmp _Find        
         
    _FindAndReplace_END:
      mov [di], '$'
      ret
  FindAndReplace ENDP
  
  
  ShowOutputStr PROC near
    call PrintNewLine
       
    lea dx, outputInfo
    mov ah, 9
    INT 21H
    
    lea dx, outputStr
    mov ah, 9
    INT 21H
    
    ret
  ShowOutputStr ENDP
  
  ; ---- End of SUBROUTINES ---- ;
