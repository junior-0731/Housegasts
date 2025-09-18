// Importamos todo lo que necesitamos para este componente
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ProductosService } from '../Productos.service';
import { ProductoDTO } from '../Productos';
import { ItemProductoComponent } from "../item-producto/item-producto.component";
import { HttpResponse } from '@angular/common/http';
import { PaginacionDTO } from '../../../compartidos/modelos/PaginacionDTO';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import { FooterComponent } from "../../../compartidos/footer/footer.component";
import { HeaderComponent } from "../../../compartidos/header/header.component";
// Este componente muestra la lista de productos con paginacion
@Component({
  selector: 'app-mostrar-productos',
  imports: [ItemProductoComponent, MatPaginatorModule, FooterComponent, HeaderComponent, RouterLink],
  templateUrl: './mostrar-productos.component.html',
  styleUrl: './mostrar-productos.component.css'
})
export class MostrarProductosComponent implements OnInit {
  // Se ejecuta cuando el componente se inicializa
  ngOnInit(): void {
    this.cargarRegistros()
  }
    
    // Inyectamos las dependencias que necesitamos
    private router = inject(Router);/* Esto nos devuelve una instancia de la clase */
    private productosService = inject(ProductosService);
    
    // Array donde guardamos los productos que vienen del backend
    public productos : ProductoDTO[] = []

    // Configuracion de paginacion - empezamos en pagina 1 con 8 productos por pagina
    paginacion: PaginacionDTO = { pagina: 1, recordsPorPagina: 8 };
    //Cantidad de registros totales que hay en la base de datos
    cantidadTotalRegistros!:number;

    constructor(){
      // Cargamos los registros cuando se crea el componente
      this.cargarRegistros();
    }
    // Metodo que carga los productos desde el backend
    // ASINCRONO: Este metodo maneja operaciones asincronas usando subscribe
    cargarRegistros (){
       // ASINCRONO: Hacemos la peticion al backend sin bloquear la UI
       // El subscribe se ejecuta cuando la peticion HTTP termina
       this.productosService.obtenerPaginado(this.paginacion).subscribe(((respuesta:HttpResponse<ProductoDTO[]>)=>{
        // Guardamos los productos en el array cuando llegan del servidor
        this.productos = respuesta.body as ProductoDTO[];
        // Obtenemos la cantidad total de registros desde la cabecera HTTP
        const cabecera = respuesta.headers.get("cantidadTotalRegistros");
        this.cantidadTotalRegistros = Number(cabecera);
        // IMPORTANTE: Angular detecta el cambio automaticamente y actualiza la UI
      }))
    }
    // Metodo que se ejecuta cuando el usuario cambia de pagina o cambia el tamaño de pagina
    // ASINCRONO: Este metodo dispara una nueva peticion asincrona
    actualizarPaginacion(datos:PageEvent){
      // Actualizamos los parametros de paginacion (pageIndex empieza en 0, por eso +1)
      this.paginacion = {pagina: datos.pageIndex + 1, recordsPorPagina: datos.pageSize};
      // ASINCRONO: Recargamos los productos con la nueva paginacion de forma asincrona
      // La UI se actualiza automaticamente cuando llegan los nuevos datos
      this.cargarRegistros();

    }

}
