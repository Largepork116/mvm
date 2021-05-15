import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotificationService } from '@app/services/others/notification.service';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { Subject } from 'rxjs';
import { DocumentManagerService } from '../../../../services/api/document-manager.service';
import { PersonService } from '../../../../services/api/person.service';
import { takeUntil } from 'rxjs/operators';
import { IPerson } from '../../../../interfaces/person.interface';
import { IDocumentType } from '../../../../interfaces/document-type.interface';
import { DOCUMENTS_TYPES } from '@app/data/document-type-const';
import { IDocument } from '../../../../interfaces/document.interface';

@Component({
  selector: 'app-create-document',
  templateUrl: './create-document.component.html',
  styleUrls: ['./create-document.component.scss']
})
export class CreateDocumentComponent implements OnInit {

  documentForm: FormGroup;
  isSubmitted: boolean;
  people: IPerson[] = [];
  documentsTypes: IDocumentType[] = DOCUMENTS_TYPES;
  subscriptions = new Subject();
  file: File = null;

  constructor(public activeModal: NgbActiveModal, 
    private formBuilder: FormBuilder,
    private documentManagerService: DocumentManagerService,
    private personService: PersonService,
    private notificationService: NotificationService,
    private modalService: NgbModal) { 

    this.documentForm = this.formBuilder.group({
        senderId: ['', Validators.required],
        addresseeId: ['', Validators.required],
        type: ['', Validators.required],
      })
  }

  errorMessages = {
    senderId: [
      { type: "required", message: "Campo requerido" }
    ],
    addresseeId: [
      { type: "required", message: "Campo requerido" }
    ],
    type: [
      { type: "required", message: "Campo requerido" }
    ]
  };

  get senderId(): FormControl {
    return this.documentForm.get("senderId") as FormControl;
  }

  get addresseeId(): FormControl {
    return this.documentForm.get("addresseeId") as FormControl;
  }

  get type(): FormControl {
    return this.documentForm.get("type") as FormControl;
  }

  ngOnInit() {
    this.getPeople();
  }

  async getPeople() {
    this.people = null;
    this.personService.get()
      .pipe(takeUntil(this.subscriptions))
      .subscribe(
        (response: IAppHttpResponse<IPerson[]>) => {
          this.people = response.data;
          console.log("DATA => ", this.people);
        },
        (error: ITrackHttpError) => {
          console.log("ERROR => ", error);
        }
      );
  }

  onFileChange(files: FileList) {
    this.file = files.item(0);
  }

  onSubmit(){
    this.isSubmitted = true;
    console.log("submit => ", this.documentForm)

    if(!this.documentForm.valid) {
      return;
    }

    if(!this.file) {
      this.notificationService.toast("Debe seleccionar un archivo", 'warning');
      return;
    }

    let document: IDocument = this.documentForm.value;

    console.log("SEND =>", document);

    this.documentManagerService.create(this.file, document)
      .subscribe(
        (response: IAppHttpResponse<any>) => {
        this.notificationService.toast("La creación del documento finalizó exitosamente", "success");
          this.activeModal.dismiss();
        },
        (error: ITrackHttpError) => {
          console.log("submit error");
          console.log(error);
        }
      )
  }
}
