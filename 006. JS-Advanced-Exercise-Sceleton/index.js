function solve(area, vol, input) {
   let toArr = JSON.parse(input);

   let arr=[];
    toArr.forEach(figure=> {
      arr.push({
         area: Math.abs(area.call(figure)),
         volume: Math.abs(vol.call(figure))
      });
   });

   return arr;
}
function area() {
   return this.x * this.y;
};

function vol() {
   return this.x * this.y * this.z;
};



let s = solve(area, vol, `[
   {"x":"10","y":"-22","z":"10"},
   {"x":"47","y":"7","z":"-5"},
   {"x":"55","y":"8","z":"0"},
   {"x":"100","y":"100","z":"100"},
   {"x":"55","y":"80","z":"250"}
   ]`);

class Person {
   constructor(firstName,lastName){
      this.firstName = firstName;
      this.lastName = lastName;
   }

   get fullName(){
      return `${this.firstName} ${this.lastName}`;
   }

   set fullName(input){
      if (/\w+ \w+/.test(input)) {
         let [firstName, lastName] = input.split(' ');

         this.firstName = firstName;
         this.lastName = lastName;
      }
   }
}

// let person = new Person("Peter", "Ivanov");
// console.log(person.fullName);//Peter Ivanov
// person.firstName = "George";
// console.log(person.fullName);//George Ivanov
// person.lastName = "Peterson";
// console.log(person.fullName);//George Peterson
// person.fullName = "Nikola Tesla";
// console.log(person.firstName)//Nikola
// console.log(person.lastName)//Tesla


function arrayMap(nums,func) {
   let mapped = nums.reduce((a,x)=> {
         a.push(func(x));

         return a;
   },[]);

   return mapped;
}

let nums = [1,2,3,4,5];
//console.log(arrayMap(nums,(item)=> item * 2)); // [ 2, 4, 6, 8, 10 ]








function Spy(obj,method){
   let originalFunc = obj[method];

   let res = {
      count:0
   };

   obj[method] = function(){
      res.count++;
      return originalFunc.apply(this,arguments);
   };

   return res;
}


let obj = {
   method:()=>"invoked"
}
//let spy = Spy(obj,"method");

// obj.method();
// obj.method();
// obj.method();

// console.log(spy) // { count: 3 }


let spy = Spy(console,"log");

console.log(spy); // { count: 1 }
console.log(spy); // { count: 2 }
console.log(spy); // { count: 3 }
