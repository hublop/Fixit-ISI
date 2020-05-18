import { Router } from '@angular/router';
import { AuthService } from './../../../auth/_services/auth.service';
import { Component, OnInit, Input } from '@angular/core';
import { CategoryInProfile } from '../../_models/CategoryInProfile';

@Component({
  selector: 'app-provided-services',
  templateUrl: './provided-services.component.html',
  styleUrls: ['./provided-services.component.scss']
})
export class ProvidedServicesComponent implements OnInit {

  @Input() providedCategories: CategoryInProfile[];
  @Input() id: number;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  hasAnyServices(): boolean {
    return this.providedCategories && this.providedCategories.length > 0;
  }

  canEditProfile(): boolean {
    const loggedInUser = this.authService.getloggedInUser();
    if (!loggedInUser) {
      return false;
    }

    return loggedInUser.id === this.id;
  }

  navigateToEdit() {
    this.router.navigate(['contractors/profile/' + this.id + '/edit', {editType: 'services'}]);
  }
}
