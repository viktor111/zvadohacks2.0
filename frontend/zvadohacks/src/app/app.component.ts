import { Component, AfterViewInit, ElementRef } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit {

  constructor(private el: ElementRef) { }

  ngAfterViewInit() {
    const canvas = this.el.nativeElement.querySelector('#matrixCanvas');
    const context = canvas.getContext('2d');

    canvas.height = window.innerHeight;
    canvas.width = window.innerWidth;

    const font_size = 10;
    let columns = canvas.width / font_size;

    const drops: number[] = [];
    for (let i = 0; i < columns; i++) {
      drops[i] = 1;
    }

    function draw() {
      context.fillStyle = 'rgba(0, 0, 0, 0.05)';
      context.fillRect(0, 0, canvas.width, canvas.height);

      context.fillStyle = '#00ff00'; 
      context.font = font_size + 'px arial';

      for (let i = 0; i < drops.length; i++) {
        // Removed the if condition here to cover entire screen
        const text = String.fromCharCode(0x30A0 + Math.random() * 33);
        context.fillText(text, i * font_size, drops[i] * font_size);

        if (drops[i] * font_size > canvas.height && Math.random() > 0.975) {
          drops[i] = 0;
        }
        drops[i]++;
      }
    }

    setInterval(draw, 33);

    window.addEventListener('resize', () => {
      canvas.height = window.innerHeight;
      canvas.width = window.innerWidth;

      // Recalculate columns based on new dimensions
      columns = canvas.width / font_size;

      // Reset the drops array
      drops.length = 0;
      for (let i = 0; i < columns; i++) {
        drops[i] = 1;
      }
    });
  }
}
