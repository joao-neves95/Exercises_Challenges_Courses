; Change every space for a "#".
;------------------------------------
;Construa uma string na Memória,
;a partir de um conjunto de carateres digitados,
;o qual deve ser terminado pressionando a tecla ENTER.
;
;Seguidamente, crie uma outra string na Memoria,
;mas substituindo cada espaço por um '#', a partir da original,
;copiando e gravando adequadamente, carater a carater,
;entre a fonte e o destino, respetivamente. 
; 
;Finalmente, mostre a nova string (sem espacos) no ecra. 
; 
;Por exemplo, se a string construida for: 'a sara e a amiga do rui' 
; 
;A string impressa no ecrã deve ser:  'a#sara#e#a#amiga#do#rui'

#make_COM#
ORG 100H
jmp Init

.Data
  newLine db 13 , 10 , '$'
  getInputInfo db 'Escreva uma frase: ', 13 , 10 , '$'
  showOutputInfo db 'A frase tratada e: ', 13 , 10 , '$'
  userInput db 80 dup(?)
  outputStr db 80 dup(?)

.Code
  Init:
    lea dx, getInputInfo
    mov ah, 9 ; Function 09H (write string to output).
    INT 21H
    
    lea si, userInput
  
  GetInput:
    mov ah, 1 ; Function 01H (read input char). In saves to the AL register.
    INT 21H
    
    mov [si], al ; The user input is stored on the AL register.
    
    cmp al, 13 ; Check if it's the end of the line.
      jz ChangeStr
      
    inc si
    jmp GetInput         
  
  ChangeStr:
    mov [si], '$'
    lea si, userInput ; Get the address of the first char of the user input.
    lea di, outputStr ; Move DI (destination index) to the first char position of the output buffer.
    ; Fall to _ChangeStr.
  
  _ChangeStr:
    cmp [si], '$' ; Check if it's the end of the line.
      jz ShowOutputStr
    
    cmp [si], ' '
      jz _AddHashtagToDI   
       
    jmp _AddCharToSI
 
  _AddCharToSI:
    mov al, [si]
    mov [di], al
    jmp _IncIdx
   
  _AddHashtagToDI:
    mov [di], '#'
    jmp _IncIdx
  
  _IncIdx:
    inc si
    inc di
    jmp _ChangeStr ; Return back. 
  
  ShowOutputStr:
    mov [di], '$' ; We have to add a end line char to the new string.
    
    lea dx, newLine
    mov ah, 9
    INT 21H
    
    lea dx, showOutputInfo
    mov ah, 9
    INT 21H
  
    lea dx, outputStr
    mov ah, 9
    INT 21H
    ; Fall to End
 
  End:
    mov ah, 04CH
    INT 21H
