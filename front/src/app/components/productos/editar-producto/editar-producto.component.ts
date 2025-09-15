import { Component, Input, numberAttribute } from '@angular/core';
import { ProductoCreacionDTO, ProductoDTO } from '../Productos';
import { FormularioProductoComponent } from '../formulario-producto/formulario-producto.component';

@Component({
  selector: 'app-editar-producto',
  imports: [FormularioProductoComponent],
  templateUrl: './editar-producto.component.html',
  styleUrl: './editar-producto.component.css'
})
export class EditarProductoComponent {
  @Input({transform:numberAttribute})
  id!:number;
  producto:ProductoCreacionDTO = { Name:"El hombre Arañ, ",Category:1, Unite:1}
  guardarCambios(producto:ProductoCreacionDTO){
    }
  
  
}
