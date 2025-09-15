import { inject, Injectable } from '@angular/core';
import { ProductoCreacionDTO, ProductoDTO } from './Productos';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
@Injectable({
  providedIn: 'root'
})
export class ProductosService {
  private http = inject(HttpClient);
  private apiURL = environment.apiURL +  "/productos";

  constructor() { }
  public obtenerTodos():Observable<ProductoDTO[]>{
    return this.http.get<ProductoDTO[]>(this.apiURL);
  }

  public crear(producto:ProductoCreacionDTO){
    return this.http.post(this.apiURL,producto);
   }
}
