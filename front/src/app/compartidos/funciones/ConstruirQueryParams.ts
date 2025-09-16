import { HttpParams } from "@angular/common/http";

export function construirQueryParams(objeto:any):HttpParams{
    let queryParams = new HttpParams();

    for(let propiedad in objeto){
        // Se estan construyendo los queryStrings con las propiedades del objeto y el valor de la propiedad
        if(objeto.hasOwnProperty(propiedad)){
            queryParams = queryParams.append(propiedad, objeto[propiedad])
        }
    }

    return queryParams;


}