; Remove every occurrence of "ana" in a string.

;Leia um conjunto de carateres digitados, ate que seja pressionada a tecla ENTER,
;com o objetivo de formar uma string na Memoria. 
; 
;Seguidamente, crie tambem na memoria uma nova string a partir da original,
;mas com a eliminacao de todas as subfrases 'ana'. 
; 
;Finalmente, mostre a nova string no ecra. 
; 
;Por exemplo, se a string construida for:          'a ana e o aniceto foram ao cinema anadia' 
; 
;A string impressa no ecra deve ser: 
; 
;  'a e o aniceto foram ao cinema dia' 

; Logica:
; 1 - Encontrar um 'a'
; 2 - Verificar se os caracteres seguintes sao 'na'
;     - Se sim, incrementar SI.
;     - Se nao, continuar a colocar na string destino.

#make_COM#
org 100h
jmp Main

.Data
  ana db 'ana' , '$'
  newLine db 13 , 10 , '$'
  getInputInfo db 'Escreva uma frase: ', 13 , 10 , '$'
  showOutputInfo db 'A frase tratada e: ', 13 , 10 , '$'
  inputStr db 80 dup(?)
  outputStr db 80 dup(?)

.Code
  Main:
    lea dx, getInputInfo
    mov ah, 9
    int 21H
    
    lea si, inputStr
  
  GetInput:
    mov ah, 1 ; Function 01H (read input char). In saves to the AL register.
    INT 21H
    
    mov [si], al ; The user input is stored on the AL register.
    
    cmp al, 13 ; Check if the user clicked <ENTER>.
      jz BuildOutput
      
    inc si
    jmp GetInput
    
  BuildOutput:
    mov [si], '$'
    lea si, inputStr
    lea di, outputStr
    ; Fall to _BuildOutput.
    
  _BuildOutput:
    cmp [si], '$'
      jz ShowOutput
    
    cmp [si], 'a'
      jz CheckIfAna
      
    mov al, [si]
    mov [di], al
    
    inc si
    inc di
    
    jmp _BuildOutput
    
  CheckIfAna:
    mov bx, si ; Move and store the current offset in BX.
    push si
    push di
    lea di, ana
    jmp _CheckIfAna
    
  ShowOutput:
    mov [di], '$'
    
    lea dx, newLine
    mov ah, 9
    INT 21H
    
    lea dx, showOutputInfo
    mov ah, 9
    INT 21H
    
    lea dx, outputStr
    mov ah, 9
    INT 21H
    ; Fall to End.
  
  End:
    mov ah, 04CH
    INT 21H

  ; ----------- CheckIfAna -----------

  _CheckIfAna:
    cmp [di], '$'; Check if it's the end of 'ana$'.
      jz _CheckedIfAna_POP ; The inner string was 'ana'.
    
    mov al, [di]
    cmp al, [bx] ; Check if it's 'ana'.
      jz _CheckIfAna_INC
    
    ; It's not 'ana'.
    ; Get all the original values again.
    pop di
    pop si
    mov al, [si]
    mov [di], al ; Move 'a' to DI.
    ; Move one char forward.
    inc si
    inc di
    jmp _BuildOutput
  
  _CheckIfAna_INC:
    inc bx
    inc di  
    jmp _CheckIfAna
  
  ; Increment the original SI to DX (after 'ana' location).
  _CheckedIfAna_POP:
    pop di
    pop si
    mov si, bx ; Jump to after 'ana' here.
    jmp _BuildOutput

  ; --------- CheckIfAna ---------
    