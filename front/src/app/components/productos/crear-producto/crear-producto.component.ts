import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ProductoCreacionDTO } from '../Productos';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormularioProductoComponent } from '../formulario-producto/formulario-producto.component';
import { ProductosService } from '../Productos.service';
import { extraerErrores } from '../../../compartidos/funciones/extraerErrores';
import { MostrarErroresComponent } from "../../../compartidos/componentes/mostrar-errores/mostrar-errores.component";
import { HeaderComponent } from "../../../compartidos/header/header.component";
import { FooterComponent } from "../../../compartidos/footer/footer.component";


@Component({
  selector: 'app-crear-producto',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, FormularioProductoComponent, MostrarErroresComponent, HeaderComponent, FooterComponent],
  templateUrl: './crear-producto.component.html',
  styleUrl: './crear-producto.component.css'
})
export class CrearProductoComponent {
    private router = inject(Router);/* Esto nos devuelve una instancia de la clase */
    private productosService = inject(ProductosService);
    errores :string[] = [];

  guardarCambios(producto: ProductoCreacionDTO){
    alert("Producto creado con exito");
    this.productosService.crear(producto).subscribe({
      next:()=>  {
        this.router.navigate(['/products'])
      },
      error: (error) => {
        const errores = extraerErrores(error);
        this.errores = errores;
      }
    
    })
  }

    

}
