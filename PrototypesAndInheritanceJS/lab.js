function personAndTeacher() {
    function Person(name, email) {
        this.name = name;
        this.email = email;
    }

    function Teacher(name, email, subject) {
        Person.call(this, name, email);
        this.subject = subject;
    }

    function Student(name, email, course) {
        Person.call(this, name, email);
        this.course = course;

    }


    Teacher.prototype = Object.create(Person.prototype);
    Student.prototype = Object.create(Person.prototype);
    Person.prototype.toString = function () {
        return `Person (name: ${this.name}, email: ${this.email})`;
    }
    Teacher.prototype.toString = function () {
        return `Teacher (name: ${this.name}, email: ${this.email}, subject: ${this.subject})`;
    }
    Student.prototype.toString = function () {
        return `Student (name: ${this.name}, email: ${this.email}, course: ${this.course})`;
    }

    return {
        Person,
        Teacher,
        Student
    }
}




// let newPerson =  new (personAndTeacher().Person)('Gosho','gosho@gmail.com');
// console.log(newPerson);

// let newTeacher = new (personAndTeacher().Teacher)('Gosho','gosho@gmail.com','Shit');
// console.log(newTeacher);



function extendClass(classToExtend) {
    classToExtend.prototype.species = 'Human';
    classToExtend.prototype.toSpeciesString = function () {
        return `I am a ${this.species}. ${this.toString()}`;
    }
    return classToExtend;
}



function solve() {
    class Figure {
        //         Figure:
        // •	Should have property units ("m", "cm", "mm") with default value "cm"
        // •	Has method changeUnits that sets different units for that figure 
        units='cm';
        modifier = 1;
        constructor(){

        }

        changeUnits(units){
            this.units = units;

            if (units==='mm') {
                this.modifier = 10;
            }else if (units === 'm') {
                 this.modifier = 0.1;
            }else if(units==='cm'){
                if (this.modifier!==1) {
                    this.modifier = this.modifier=== 10 ? 0.1 : 10;
                }
            }

        } 
    }

    class Circle extends Figure{
        constructor(radius,units='cm'){
            super();
            this.radius = Number(radius);
            this.changeUnits(units);
        }

        changeUnits(units){
            super.changeUnits(units);
            this.radius = this.radius * this.modifier;
        }

        get area(){
            return Math.PI*this.radius** 2;
        }

        toString(){
            return `Figures units: ${this.units} Area: ${this.area} - radius: ${this.radius}`;
        }
    }

    class Rectangle extends Figure{
        constructor(width,height,units='cm'){
            super();
            this.width = Number(width);
            this.height= Number(height);
            this.changeUnits(units);
        }

        changeUnits(units) {
            super.changeUnits(units);
            this.width = this.width*this.modifier;
            this.height = this.height* this.modifier;
        }

        get area(){
            return this.width*this.height;
        }


        toString(){
            return `Figures units: ${this.units} Area: ${this.area} - width: ${this.width}, height: ${this.height}`;
        }
    }


    return {
        Figure,
        Circle,
        Rectangle
    }
}


let res = solve();
let c = new res.Circle(5);
console.log(c.area); // 78.53981633974483
console.log(c.toString()); // Figures units: cm Area: 78.53981633974483 - radius: 5

let r = new res.Rectangle(3, 4, 'mm');
console.log(r.area); // 1200 
console.log(r.toString()); //Figures units: mm Area: 1200 - width: 30, height: 40

r.changeUnits('cm');
console.log(r.area); // 12
console.log(r.toString()); // Figures units: cm Area: 12 - width: 3, height: 4

c.changeUnits('mm');
console.log(c.area); // 7853.981633974483
console.log(c.toString()) // Figures units: mm Area: 7853.981633974483 - radius: 50
