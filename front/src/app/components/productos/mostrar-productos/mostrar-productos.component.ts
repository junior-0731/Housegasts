import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductosService } from '../Productos.service';
import { ProductoDTO } from '../Productos';
import { ItemProductoComponent } from "../item-producto/item-producto.component";
import { HttpResponse } from '@angular/common/http';
import { PaginacionDTO } from '../../../compartidos/modelos/PaginacionDTO';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';
@Component({
  selector: 'app-mostrar-productos',
  imports: [ItemProductoComponent, MatPaginatorModule],
  templateUrl: './mostrar-productos.component.html',
  styleUrl: './mostrar-productos.component.css'
})
export class MostrarProductosComponent {
    private router = inject(Router);/* Esto nos devuelve una instancia de la clase */
    private productosService = inject(ProductosService);
    public productos : ProductoDTO[] = []

    paginacion: PaginacionDTO = { pagina: 1, recordsPorPagina: 5 };
    //Cantidad de registros
    cantidadTotalRegistros!:number;

    constructor(){
      this.cargarRegistros();
      
    }
    cargarRegistros (){
       this.productosService.obtenerPaginado(this.paginacion).subscribe(((respuesta:HttpResponse<ProductoDTO[]>)=>{
        this.productos = respuesta.body as ProductoDTO[];
        const cabecera = respuesta.headers.get("cantidadTotalRegistros");
        this.cantidadTotalRegistros = Number(cabecera);
      }))
    }
    actualizarPaginacion(datos:PageEvent){
      this.paginacion = {pagina: datos.pageIndex + 1, recordsPorPagina: datos.pageSize};
      this.cargarRegistros();

    }

}
