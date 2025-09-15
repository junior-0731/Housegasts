import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
// Crearemos una funcion que retorna 
export function primeraLetraMayuscula():ValidatorFn{
    //Retornaremos una funcion anonima la cual va recibir como parametro un control de tipo Abstractontrol que retorna ValidationErrors o null
    return (control:AbstractControl):ValidationErrors | null=>{
        // Aca estamos transformando en string el valor del control
        const valor = <string>control.value;
        if(!valor) return null;
        if(valor.length ===0) return null;
        const primeraLetra = valor[0]
        // SI la primera letra no es mayuscula retornar una clave o llave que contiene un mensaje, osea que en el la clase se usa .hasError('primeraLetraMayuscula')
        if(primeraLetra !== primeraLetra.toUpperCase()){
            return {
                primeraLetraMayuscula:{
                    mensaje:'La Primera letra debe ser mayuscula'
                }
            }
        }
        return null
        

    }


}