import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { APIDefinition, Columns, Config, DefaultConfig } from 'ngx-easy-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ThemeService } from '../../../services/others/theme.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DocumentManagerService } from '../../../services/api/document-manager.service';
import { CreateDocumentComponent } from './create-document/create-document.component';
import { IDocument } from '../../../interfaces/document.interface';

@Component({
  selector: 'app-document-manager',
  templateUrl: './document-manager.component.html',
  styleUrls: ['./document-manager.component.scss']
})

export class DocumentManagerComponent implements OnInit {

  @ViewChild('table', { static: true }) table: APIDefinition;

  public columns: Columns[] = [
    { key: '', title: '', width: '10px', orderEnabled: false, searchEnabled: false },
    { key: 'consecutive', title: 'Consecutivo', width: '30%' },
    { key: 'sender', title: 'Remitente', width: '30%' },
    { key: '', title: 'Destinatario', width: '30%' },
    { key: '', title: '', width: '90px', orderEnabled: false, searchEnabled: false },
  ];

  public collection: any[];
  public configuration: Config;
  public toggledRows = new Set<number>();
  subscriptions = new Subject();

  constructor(private documentManagerService: DocumentManagerService,
    private themeService: ThemeService,
    private router: Router,
    private modalService: NgbModal) { 

  }

  ngAfterContentInit(): void {
    this.getDocuments();
  }

  ngOnInit(): void {
    this.configuration = { ...DefaultConfig };
    this.configuration.detailsTemplate = true;
    this.configuration.showDetailsArrow = false;
    this.configuration.paginationRangeEnabled = false;
    this.configuration.paginationMaxSize = 7;
    this.configuration.paginationEnabled = false;
  }

  async getDocuments() {
    this.collection = null;
    this.documentManagerService.get()
      .pipe(takeUntil(this.subscriptions))
      .subscribe(
        (response: IAppHttpResponse<IDocument[]>) => {
          this.collection = response.data;
          console.log("DATA => ", this.collection);
        },
        (error: ITrackHttpError) => {
          console.log("ERROR => ", error);
        }
      );
  }

  create(): void {
    const modal = this.modalService.open(CreateDocumentComponent, { size: 'lg', backdrop: 'static', windowClass:`app-${this.themeService.theme}`});

    modal.result.then((result) => {
      console.log("Result => ", result);
    }, (reason) => {
      if (reason === ModalDismissReasons.ESC) {
        console.log('by pressing ESC');
      } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
        console.log('by clicking on a backdrop');
      } else {
        this.getDocuments();
        console.log(`with: ${reason}`);
      }
    });
  }

  view(document: IDocument) {

    var fileName = ""
    if(document.externalFile) {
      fileName = document.externalFile.path
    }

    if(document.internalFile) {
      fileName = document.internalFile.path
    }


    this.documentManagerService.getPdf(fileName);
  }

}
