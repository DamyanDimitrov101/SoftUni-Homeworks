function getArticleGenerator(articles) {
   let content = document.getElementById('content');

   function showNext() {
      if (articles.length > 0) {
         let article = document.createElement('article');
         article.textContent = articles.shift();

         content.appendChild(article);
      }
   }

   return showNext;
}

















function solve() {
   let elementTbody = document.querySelector('tbody');
   elementTbody.addEventListener('click', onSelect);

   let clicked;

   function onSelect(e) {
      let selectedTd = e.target;
      if (selectedTd.tagName === "TD") {
         let selectedTr = selectedTd.parentElement;

         [...selectedTr.children].forEach(td => {

            if (clicked === selectedTr) {
               td.style.backgroundColor = '';
            } else {
               if (clicked !== undefined) {
                  [...clicked.children].forEach(td => { td.style.backgroundColor = ''; });

               }
               td.style.backgroundColor = "#413f5e";
            }
         });

         clicked = selectedTr;
      }
   }
}






















class Company {
   constructor() {
      this.departments = {};
   }

   addEmployee(username, salary, position, department) {
      if (!username || !salary || !position || !department || salary < 0) {
         throw new Error("Invalid input!");
      }

      if (!this.departments[department]) {
         this.departments[department] = [];
      }

      this.departments[department]
         .push({ username: username, salary: salary, position: position });

      return `New employee is hired. Name: ${username}. Position: ${position}`;
   }

   bestDepartment() {
      let sorted = Object.entries(this.departments)
         .sort(x => (x.reduce((a, x) => a + x.salary, 0)) / x.length)[0];

      let output = '';

      output += (`Best Department is: ${sorted[0]}\n`);
      output += (`Average salary: ${((sorted[1].reduce((a, x) => a + x.salary, 0)) / sorted[1].length).toFixed(2)}\n`);
      sorted[1]
         .sort((a, b) => b.salary - a.salary || a.username.localeCompare(b.username))
         .forEach(p => {
            output += (`${p.username} ${p.salary} ${p.position}\n`);
         });
      return output.trim();
   }
}

// let c = new Company();
// c.addEmployee("Stanimir", 2000, "engineer", "Construction");
// c.addEmployee("Pesho", 1500, "electrical engineer", "Construction");
// c.addEmployee("Slavi", 500, "dyer", "Construction");
// c.addEmployee("Stan", 2000, "architect", "Construction");
// c.addEmployee("Stanimir", 1200, "digital marketing manager", "Marketing");
// c.addEmployee("Pesho", 1000, "graphical designer", "Marketing");
// c.addEmployee("Gosho", 1350, "HR", "Human resources");
// console.log(c.bestDepartment());

function getFibonator() {
   let currentNumber = 0;
   let nextNumber = 1;

   function fib() {
      let temp = currentNumber;
      currentNumber += nextNumber;
      nextNumber = temp;

      return currentNumber;
   }

   return fib;
}


// let fib = getFibonator();
// console.log(fib()); // 1
// console.log(fib()); // 1
// console.log(fib()); // 2
// console.log(fib()); // 3
// console.log(fib()); // 5
// console.log(fib()); // 8
// console.log(fib()); // 13


class Hex {

   constructor(value) {
      this.value = value;
   }

   get hexValue() {
      return this.value.toString(16).toUpperCase();
   }

   set hexValue(value) {
      this.hexValue = value;
   }

   valueOf() {
      return this.value;
   }

   toString() {
      return `0x${this.value.toString(16).toUpperCase()}`;
   }

   plus(number) {
      let res = this.value + parseInt(number, 16);

      return new Hex(res);
   }

   minus(number) {
      let res = this.value - parseInt(number, 16);
      return new Hex(res);
   }

   parse(string) {
      return parseInt(string, 16);
   }
}

// let FF = new Hex(255);
// console.log(FF.toString());
// FF.valueOf() + 1 == 256;
// let a = new Hex(10);
// let b = new Hex(5);
// console.log(a.plus(b).toString());
// console.log(a.plus(b).toString()==='0xF');


