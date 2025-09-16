import { inject, Injectable } from '@angular/core';
import { ProductoCreacionDTO, ProductoDTO } from './Productos';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { PaginacionDTO } from '../../compartidos/modelos/PaginacionDTO';
import { construirQueryParams } from '../../compartidos/funciones/ConstruirQueryParams';
@Injectable({
  providedIn: 'root'
})
export class ProductosService {
  private http = inject(HttpClient);
  private apiURL = environment.apiURL +  "/productos";

  constructor() { }
  public obtenerPaginado(paginacion:PaginacionDTO):Observable<HttpResponse<ProductoDTO[]>>{
    let queryParams = construirQueryParams(paginacion);
    return this.http.get<ProductoDTO[]>(this.apiURL, {params:queryParams, observe:'response'});
  }

  public crear(producto:ProductoCreacionDTO){
    return this.http.post(this.apiURL,producto);
  }
  // Obtener un producto por su ID
  public obtenerPorId(id:number):Observable<ProductoDTO>{
   return this.http.get<ProductoDTO>(`${this.apiURL}/${id}`);
  }
  public actualizar(id:number, producto:ProductoCreacionDTO){
   return this.http.put(`${this.apiURL}/${id}`, producto);
  }

  public eliminar(id:number){{
    return this.http.delete(`${this.apiURL}/${id}`);
   }
  }
}