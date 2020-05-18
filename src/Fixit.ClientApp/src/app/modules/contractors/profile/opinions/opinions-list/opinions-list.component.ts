import { Component, OnInit, Input } from '@angular/core';
import { OpinionInProfile } from '../../../_models/OpinionInProfile';

@Component({
  selector: 'app-opinions-list',
  templateUrl: './opinions-list.component.html',
  styleUrls: ['./opinions-list.component.scss']
})
export class OpinionsListComponent implements OnInit {

  @Input() opinions: OpinionInProfile[];
  displayedOpinions: OpinionInProfile[];
  displayedOpinionsCount = 5;

  constructor() {
    this.displayedOpinions = new Array<OpinionInProfile>();
  }

  ngOnInit() {
    this.displayOpinions();
  }

  showAllOpinions(): void {
    this.displayedOpinionsCount = this.opinions ? this.opinions.length : 0;
    this.displayOpinions();
  }

  displayOpinions(): void {
    this.displayedOpinions = new Array<OpinionInProfile>();
    for (let i = 0; i < this.displayedOpinionsCount && i < this.opinions.length; i++) {
      this.displayedOpinions.push(this.opinions[i]);
    }
  }

  showAllOpinionsVisible(): boolean {
    return this.opinions && this.opinions.length > this.displayedOpinionsCount;
  }

  hasOpinions(): boolean {
    return this.opinions && this.opinions.length > 0;
  }
}
