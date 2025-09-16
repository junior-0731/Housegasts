import { Component, Input } from '@angular/core';
import { ProductoDTO } from '../Productos';

@Component({
  selector: 'app-item-producto',
  imports: [],
  templateUrl: './item-producto.component.html',
  styleUrl: './item-producto.component.css'
})
export class ItemProductoComponent {
  @Input({required:true})
  producto!:ProductoDTO;
}
