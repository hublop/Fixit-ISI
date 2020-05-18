import { Component, Input, OnInit } from '@angular/core';

import { OpinionInProfile } from '../../_models/OpinionInProfile';
import { Router } from '../../../../../../node_modules/@angular/router';
import { AuthService } from '../../../auth/_services/auth.service';

@Component({
  selector: 'app-opinions',
  templateUrl: './opinions.component.html',
  styleUrls: ['./opinions.component.scss']
})
export class OpinionsComponent implements OnInit {
  title: 'Opinie';

  @Input() avgQuality: number;
  @Input() avgPunctuality: number;
  @Input() avgInvolvement: number;
  @Input() avgRating: number;
  @Input() id: number;
  @Input() opinions: OpinionInProfile[];

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit() {
  }

  getOpinionsCount() {
    return this.opinions ? this.opinions.length : 0;
  }

  addOpinionButtonVisible(): boolean {
    const loggedInUser = this.authService.getloggedInUser();
    if (!loggedInUser) {
      return true;
    }

    return loggedInUser.id !== this.id && !this.authService.isContractor();
  }

  navigateToAddOpinion(): void {
    if (this.authService.isLoggedIn() && this.authService.getloggedInUser() &&
      this.authService.getloggedInUser().id === this.id) {
      return;
    }
    this.router.navigate(['/contractors/profile/' + this.id + '/opinions/add']);
  }
}
