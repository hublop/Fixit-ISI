import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss']
})
export class InfoComponent implements OnInit {

  @Input() title: string;
  @Input() isSuccess: boolean;

  constructor() { }

  ngOnInit() {
  }

  getStyle() {
    const styles = {
      'color': this.isSuccess ? '#19a500' : '#c61717'
    };

    return styles;
  }
}
