import { Injectable } from '@angular/core';
import Swal, { SweetAlertResult } from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

constructor() { }

  toast(title: string, icon: "error" | "warning" | "success" | "info" | "question"){
    const Toast = Swal.mixin({
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
      didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
      }
    })
    
    Toast.fire({
      icon: icon,
      title: title
    })
  }

  confirm(title?: string, text?: string, icon?: "error" | "warning" | "success" | "info" | "question") : Promise<SweetAlertResult<any>>{
    return Swal.fire({
      title: title,
      text: text,
      icon: icon,
      showCancelButton: true,
      cancelButtonText: 'Cancelar',
      confirmButtonText: 'Continuar',
    })
  }

  confirmAlert(title?: string, text?: string, icon?: "error" | "warning" | "success" | "info" | "question") : Promise<SweetAlertResult<any>>{
    return Swal.fire({
      title: title,
      text: text,
      icon: icon,
      showCancelButton: false,
      confirmButtonText: 'Continuar',
    })
  }

  confirmAlertHtml(title?: string, text?: string, icon?: "error" | "warning" | "success" | "info" | "question") : Promise<SweetAlertResult<any>>{
    return Swal.fire({
      title: title,
      html: text,
      icon: icon,
      showCancelButton: false,
      confirmButtonText: 'Continuar',  
      showClass: {
        popup: 'animate__animated animate__fadeInDown'
      },
      hideClass: {
        popup: 'animate__animated animate__fadeOutUp'
      }
    })
  }

}
