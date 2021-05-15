import { AfterContentInit, Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IAppHttpResponse, ITrackHttpError } from '@app/interfaces/app-http-response.interface';
import { UserService } from '@app/services/api/user.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { API, APIDefinition, Columns, Config, DefaultConfig } from 'ngx-easy-table';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { CreateUserComponent } from './create-user/create-user.component';
import { ThemeService } from '../../../services/others/theme.service';
import { EditUserComponent } from './edit-user/edit-user.component';
import { IUser } from '../../../interfaces/user.interface';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit, AfterContentInit {

  @ViewChild('table', { static: true }) table: APIDefinition;

  public columns: Columns[] = [
    { key: '', title: '', width: '10px', orderEnabled: false, searchEnabled: false },
    { key: 'name', title: 'Nombre', width: '50%' },
    { key: 'email', title: 'Email', width: '30%' },
    { key: 'role', title: 'Rol', width: '15%' },
    { key: '', title: '', width: '90px', orderEnabled: false, searchEnabled: false },
  ];

  public collection: any[];
  public configuration: Config;
  public toggledRows = new Set<number>();
  subscriptions = new Subject();

  constructor(private modalService: NgbModal, 
    private userService: UserService,
    private themeService: ThemeService,
    private router: Router) { 

  }

  ngAfterContentInit(): void {
    this.getUsers();
  }

  ngOnInit(): void {
    this.configuration = { ...DefaultConfig };
    this.configuration.detailsTemplate = true;
    this.configuration.showDetailsArrow = false;
    this.configuration.paginationRangeEnabled = false;
    this.configuration.paginationMaxSize = 7;
    this.configuration.paginationEnabled = false;
  }

  async getUsers() {
    this.collection = null;
    this.userService.get()
      .pipe(takeUntil(this.subscriptions))
      .subscribe(
        (response: IAppHttpResponse<IUser[]>) => {
          this.collection = response.data.filter(x=> x.userId != 1)
          console.log("DATA => ", this.collection);
        },
        (error: ITrackHttpError) => {
          console.log("ERROR => ", error);
        }
      );
  }

  expand($event, index){
    $event.preventDefault();
    this.table.apiEvent({
      type: API.toggleRowIndex,
      value: index,
    });
    if (this.toggledRows.has(index)) {
      this.toggledRows.delete(index);
    } else {
      this.toggledRows.add(index);
    }
  }

  edit(row): void {
    console.log("edit => ", row)

    const modal = this.modalService.open(EditUserComponent, { size: 'lg', backdrop: 'static', windowClass:`app-${this.themeService.theme}`});
    modal.componentInstance.edit = row;

    modal.result.then((result) => {
      console.log("Result => ", result);
    }, (reason) => {
      if (reason === ModalDismissReasons.ESC) {
        console.log('by pressing ESC');
      } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
        console.log('by clicking on a backdrop');
      } else {
        this.getUsers();
        console.log(`with: ${reason}`);
      }
    });
  }

  create(): void {
    console.log("addUsers => ")

    const modal = this.modalService.open(CreateUserComponent, { size: 'lg', backdrop: 'static', windowClass:`app-${this.themeService.theme}`});

    modal.result.then((result) => {
      console.log("Result => ", result);
    }, (reason) => {
      if (reason === ModalDismissReasons.ESC) {
        console.log('by pressing ESC');
      } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
        console.log('by clicking on a backdrop');
      } else {
        this.getUsers();
        console.log(`with: ${reason}`);
      }
    });
  }

}
