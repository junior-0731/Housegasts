// Importamos todo lo que necesitamos para hacer peticiones HTTP
import { inject, Injectable } from '@angular/core';
import { ProductoCreacionDTO, ProductoDTO } from './Productos';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { PaginacionDTO } from '../../compartidos/modelos/PaginacionDTO';
import { construirQueryParams } from '../../compartidos/funciones/ConstruirQueryParams';
// Este servicio maneja todas las operaciones de productos con el backend
// IMPORTANTE: Todas las operaciones son ASINCRONAS usando Observables
// Los Observables permiten manejar datos que llegan en el tiempo sin bloquear la UI
@Injectable({
  providedIn: 'root' // Esto hace que sea un singleton, solo una instancia en toda la app
})
export class ProductosService {
  // Inyectamos HttpClient para hacer peticiones HTTP
  // HttpClient maneja las peticiones de forma asincrona automaticamente
  private http = inject(HttpClient);
  // URL base de la API de productos
  private apiURL = environment.apiURL +  "/productos";

  constructor() { }
  
  // METODO GET - Obtener productos con paginacion
  // ASINCRONO: Retorna un Observable que emite datos cuando la peticion HTTP termina
  // El Observable permite suscribirse y recibir los datos sin bloquear la UI
  public obtenerPaginado(paginacion:PaginacionDTO):Observable<HttpResponse<ProductoDTO[]>>{
    // Convertimos los parametros de paginacion a query params
    let queryParams = construirQueryParams(paginacion);
    // ASINCRONO: http.get() hace la peticion sin bloquear el hilo principal
    // observe:'response' nos da acceso a headers y status code
    return this.http.get<ProductoDTO[]>(this.apiURL, {params:queryParams, observe:'response'});
  }

  // METODO POST - Crear un nuevo producto
  // ASINCRONO: Retorna un Observable que se completa cuando el producto se crea
  // La UI no se bloquea mientras espera la respuesta del servidor
  public crear(producto:ProductoCreacionDTO){
    return this.http.post(this.apiURL,producto);
  }
  // METODO GET - Obtener un producto por su ID
  // ASINCRONO: Observable emite el producto cuando la peticion HTTP termina
  // Esto permite que la UI siga funcionando mientras busca el producto
  public obtenerPorId(id:number):Observable<ProductoDTO>{
   return this.http.get<ProductoDTO>(`${this.apiURL}/${id}`);
  }
  // METODO PUT - Actualizar un producto existente
  // ASINCRONO: Observable se completa cuando la actualizacion termina
  // La UI puede mostrar un loading mientras espera la confirmacion
  public actualizar(id:number, producto:ProductoCreacionDTO){
   return this.http.put(`${this.apiURL}/${id}`, producto);
  }

  // METODO DELETE - Eliminar un producto
  // ASINCRONO: Observable se completa cuando la eliminacion termina
  // Esto permite actualizar la UI inmediatamente sin esperar la respuesta
  public eliminar(id:number){
    return this.http.delete(`${this.apiURL}/${id}`);
  }
}