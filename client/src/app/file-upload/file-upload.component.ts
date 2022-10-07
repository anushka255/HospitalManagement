import { Component, OnInit } from '@angular/core';
import { FileUploadService } from '../_services/FileUploadService';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {
  file:File;

  constructor(private fileUploadService: FileUploadService) { }

  ngOnInit(): void {
  }

}
