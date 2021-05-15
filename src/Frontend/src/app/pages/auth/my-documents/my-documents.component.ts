import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { IDocument } from '@app/interfaces/document.interface';
import { DocumentManagerService } from '@app/services/api/document-manager.service';
import { ThemeService } from '@app/services/others/theme.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { APIDefinition, Columns, Config, DefaultConfig } from 'ngx-easy-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-my-documents',
  templateUrl: './my-documents.component.html',
  styleUrls: ['./my-documents.component.scss']
})
export class MyDocumentsComponent implements OnInit {

  @ViewChild('table', { static: true }) table: APIDefinition;

  public columns: Columns[] = [
    { key: '', title: '', width: '10px', orderEnabled: false, searchEnabled: false },
    { key: 'consecutive', title: 'Consecutivo', width: '30%' },
    { key: 'sender', title: 'Remitente', width: '30%' },
    { key: 'createdAt', title: 'Fecha y hora', width: '30%' },
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
    this.documentManagerService.getMyDocuments()
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
