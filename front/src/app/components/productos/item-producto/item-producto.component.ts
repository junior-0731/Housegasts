// Importamos todo lo que necesitamos para este componente
import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { ProductoDTO } from '../Productos';
import { Router } from '@angular/router';
import { ProductosService } from '../Productos.service';
import { extraerErrores } from '../../../compartidos/funciones/extraerErrores';

// Este componente muestra un producto individual con botones para editar y eliminar
@Component({
  selector: 'app-item-producto',
  imports: [],
  templateUrl: './item-producto.component.html',
  styleUrl: './item-producto.component.css'
})
export class ItemProductoComponent {
  // Inyectamos las dependencias que necesitamos
  public productosService = inject(ProductosService);
  public router = inject(Router);
  
  // Evento que se emite cuando se elimina un producto
  @Output()
  productoEliminado = new EventEmitter<void>;
  
  // Input que recibe el producto desde el componente padre
  @Input({required:true})
  producto!:ProductoDTO;
  
  // Array para guardar errores si los hay
  errores!: string[];
  // Metodo para navegar a la pagina de editar producto
  actualizar(){
    this.router.navigateByUrl(`/products/editar/${this.producto.id}`);
  }
  // Metodo para eliminar un producto
  // ASINCRONO: Este metodo maneja la eliminacion de forma asincrona
  eliminar(id:number){
    // ASINCRONO: Hacemos la peticion DELETE al backend sin bloquear la UI
    // El subscribe se ejecuta cuando la peticion HTTP termina (exitoso o con error)
    this.productosService.eliminar(id).subscribe({
      next:async()=>{
        // Si se elimina correctamente, emitimos el evento para que el padre actualice la lista
        // ASINCRONO: El evento se emite de forma asincrona para actualizar la UI
        this.productoEliminado.emit();
      },
        error: (error) => {
          // Si hay error, lo guardamos para mostrarlo al usuario
          // ASINCRONO: Los errores se manejan de forma asincrona tambien
          const errores = extraerErrores(error);
          this.errores = errores;
        }
    })
  }

 
}
