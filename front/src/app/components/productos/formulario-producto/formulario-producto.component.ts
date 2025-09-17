import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ProductoCreacionDTO, ProductoDTO } from '../Productos';
import { primeraLetraMayuscula } from '../../../compartidos/funciones/validaciones';
import {  ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterLink } from '@angular/router';
@Component({
  selector: 'app-formulario-producto',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, RouterLink],
  templateUrl: './formulario-producto.component.html',
  styleUrl: './formulario-producto.component.css'
})
export class FormularioProductoComponent implements OnInit {
  public router = inject(Router);
  ngOnInit(): void {
    if(this.modelo !== undefined){
      this.form.patchValue(this.modelo);
    }
  }
  @Input()
  modelo:ProductoCreacionDTO | undefined;
  private formbuilder = inject(FormBuilder)
  @Output()
  posteoFormulario = new EventEmitter<ProductoCreacionDTO>()
  
  form = this.formbuilder.group({
    name:['', {validators:[Validators.required, primeraLetraMayuscula()]}],
    // TODO: Implementar carga dinámica de categorías desde el backend
    category:[0, {validators:[Validators.required]}],
    unite:[0, {validators:[Validators.required]}],
  }
  )

  obtenerErrorCampoNombre():string{
    let nombre = this.form.controls.name;
    if(nombre.hasError('required')){
      return 'El nombre del producto es requerido'
    }
    if(nombre.hasError('primeraLetraMayuscula')){
      return nombre.getError('primeraLetraMayuscula').mensaje
    }
    return ''
  }

  obtenerErrorCampoCategoria():string{
    let categoria = this.form.controls.category;
    if(categoria.hasError('required')){
      return 'Debes seleccionar una categoría'
    }
    if(categoria.value === 0){
      return 'Selecciona una categoría válida'
    }
    return ''
  }

  obtenerErrorCampoUnidades():string{
    let unidades = this.form.controls.unite;
    if(unidades.hasError('required')){
      return 'La cantidad de unidades es requerida'
    }
    if(unidades.hasError('min')){
      return 'La cantidad debe ser mayor a 0'
    }
    return ''
  }
  guardarCambios(){
    if(!this.form.valid){
      return
    }
    const producto = this.form.value as ProductoCreacionDTO ;
    this.posteoFormulario.emit(producto)
    
  }

}
