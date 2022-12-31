import { Injectable } from "@angular/core";


@Injectable()
export class AlertService  {

  alertPlaceholder = document.getElementById('liveAlertPlaceholder')

  alert(message: string, type: string){
    const wrapper = document.createElement('div')
    wrapper.innerHTML = [
      `<div id='alertNotification' class="alert alert-${type} alert-dismissible" role="alert">`,
      `   <div>${message}</div>`,
      '   <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>',
      '</div>'
    ].join('');
    this.alertPlaceholder?.append(wrapper);
    setTimeout(this.disalert, 10000)
  }

  disalert(){
    var notification = document.getElementById('alertNotification');
    if(notification != null){
      notification.outerHTML = "";
    }
  }
}
