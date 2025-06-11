import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

@Component({
  selector: 'app-drop-down-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './drop-down-list.component.html',
  styleUrl: './drop-down-list.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropDownListComponent),
      multi: true
    }
  ]
})
export class DropDownListComponent implements ControlValueAccessor {
  @Input() items: string[] = [];
  @Input() placeholder: string = 'Select item';
  @Input() required = false;
  @Input() name = '';

  @Output() selected = new EventEmitter<string>();

  isOpen = false;
  selectedItem: string | null = null;

  onChange = (value: string) => {};
  onTouched = () => {};

  toggleDropdown(): void {
    this.isOpen = !this.isOpen;
  }

  selectItem(item: string): void {
    this.selectedItem = item;
    this.isOpen = false;
    this.onChange(item);
    this.onTouched();
    this.selected.emit(item);
  }

  // ControlValueAccessor methods
  writeValue(value: string): void {
    this.selectedItem = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
}
