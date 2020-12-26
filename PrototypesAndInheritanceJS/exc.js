(function solve() {
    Array.prototype.last = function () { return this[this.length - 1] };

    Array.prototype.skip = function (n) { return this.slice(n) };

    Array.prototype.take = function (n) { return this.slice(0, n) };

    Array.prototype.sum = function () { return this.reduce((a, x) => a + x, 0) };

    Array.prototype.average = function () { return this.sum() / this.length };
})();

// let myArr = [1,2,3];

// console.log(myArr.average());


function solve() {
    class Balloon {
        constructor(color, gasWeight) {
            this.color = color;
            this.gasWeight = Number(gasWeight);
        }
        // Implement a class Balloon, which is initialized with a color (String) and gasWeight (Number). These two arguments should be public members.
    }

    class PartyBalloon extends Balloon {
        constructor(color, gasWeight, ribbonColor, ribbonLength) {
            super(color, gasWeight);
            this._ribbon = { color: ribbonColor, length: ribbonLength };
        }

        get ribbon() {
            return this._ribbon;
        }
        //         mplement another class PartyBalloon, which inherits the Balloon class and is initialized with 2 additional parameters - ribbonColor (String) and ribbonLength (Number).
        // The PartyBalloon class should have a property ribbon, which is an object with color and length - the ones given upon initialization. The ribbon property should have a getter.
    }

    class BirthdayBalloon extends PartyBalloon {
        constructor(color, gasWeight, ribbonColor, ribbonLength, text) {
            super(color, gasWeight, ribbonColor, ribbonLength);
            this._text = text;
        }

        get text() {
            return this._text;
        }
        // Implement another class BirthdayBalloon, which inherits the PartyBalloon class and is initialized with 1 extra parameter - text (String). The text should be a property and should have a getter.
    }

    return {
        Balloon,
        PartyBalloon,
        BirthdayBalloon
    }
}



function solve() {
    class Employee {
        constructor(name, age) {
            if (new.target === Employee) {
                throw new Error('Canot instantiate directly.');
            }
            this.name = name;
            this.age = age;
            this.salary = 0;
            this.tasks = [];
        }

        work() {
            let task = this.tasks.shift();
            console.log(`${this.name} ${task}`);
            this.tasks.push(task);
        }

        collectSalary() {
            console.log(`${this.name} received ${this.getSalary()} this month.`);
        }

        getSalary() {
            return this.salary;
        }
    }

    class Junior extends Employee {
        constructor(name, age) {
            super(name, age);
            this.salary = 0;
            this.tasks.push('is working on a simple task.');
        }
    }
    class Senior extends Employee {
        constructor(name, age) {
            super(name, age);
            this.salary = 0;
            this.tasks.push('is working on a complicated task.');
            this.tasks.push('is taking time off work.');
            this.tasks.push('is supervising junior workers.');
        }
    }
    class Manager extends Employee {
        constructor(name, age) {
            super(name, age);
            this.dividend = 0;
            this.salary = 0;
            this.tasks.push('scheduled a meeting.');
            this.tasks.push('is preparing a quarterly report.');
        }

        getSalary() {
            return this.salary + this.dividend;
        }
    }


    return {
        Employee,
        Junior,
        Senior,
        Manager
    }
}


function solve() {
    class Post {
        constructor(title, content) {
            this.title = title;
            this.content = content;
        }

        toString() {
            let res = `Post: ${this.title}\nContent: ${this.content}`;
            return res;
        }
    }



    class SocialMediaPost extends Post {
        constructor(title, content, likes, dislikes) {
            super(title, content);

            this.likes = likes;
            this.dislikes = dislikes;
            this.comments = [];
        }

        addComment(comment) {
            this.comments.push(comment);
        }

        toString() {
            let res = super.toString();

            res += `\nRating: ${this.likes - this.dislikes}`;

            if (this.comments.length > 0) {
                res += '\nComments:\n';
                res += this.comments.map(c => ` * ${c}`).join('\n');
            }

            return res.trim();
        }
    }

    class BlogPost extends Post {
        constructor(title, content, views) {
            super(title, content);
            this.views = views;
        }

        view() {
            this.views++;
            return this;
        }

        toString() {
            let res = super.toString();
            res += `\nViews: ${this.views}`;
            return res.trim();
        }
    }


    return {
        Post,
        SocialMediaPost,
        BlogPost
    }
}

// let res = solvee();
// let post = new res.Post("Post", "Content");

// console.log(post.toString());

// // Post: Post
// // Content: Content

// let scm = new res.SocialMediaPost("TestTitle", "TestContent", 25, 30);

// scm.addComment("Good post");
// scm.addComment("Very good post");
// scm.addComment("Wow!");

// console.log(scm.toString());

// // Post: TestTitle
// // Content: TestContent
// // Rating: -5
// // Comments:
// //  * Good post
// //  * Very good post
// //  * Wow!



function createComputerHierarchy() {
    class Keyboard {
        constructor(manufacturer, responseTime) {
            this.manufacturer = manufacturer;
            this.responseTime = Number(responseTime);
        }
    }

    class Monitor {
        constructor(manufacturer, width, height) {
            this.manufacturer = manufacturer;
            this.width = Number(width);
            this.height = Number(height);
        }
    }

    class Battery {
        constructor(manufacturer, expectedLife) {
            this.manufacturer = manufacturer;
            this.expectedLife = Number(expectedLife);
        }
    }


    class Computer {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace) {
            if (new.target == Computer) {
                throw new Error('Attempting to instantiate an abstract class!');
            }

            this.manufacturer = manufacturer;
            this.processorSpeed = Number(processorSpeed);
            this.ram = Number(ram);
            this.hardDiskSpace = Number(hardDiskSpace);
        }
    }

    class Laptop extends Computer {
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace, weight, color, battery) {
            super(manufacturer, processorSpeed, ram, hardDiskSpace);
            this.weight = weight;
            this.color = color;
            this.battery = battery;
        }

        get battery() {
            return this._battery;
        }

        set battery(value) {
            if (value instanceof Battery) {
                this._battery = value;
            }else{
                throw new TypeError('Attempting to pass an object that is not of the expected instance!');
            }
        }
    }

    class Desktop extends Computer{
        constructor(manufacturer, processorSpeed, ram, hardDiskSpace,keyboard,monitor) {
            super(manufacturer,processorSpeed,ram,hardDiskSpace);
            this.keyboard = keyboard;
            this.monitor = monitor;
        }

        get keyboard(){
            return this._keyboard;
        }

        set keyboard(value){
            if (value instanceof Keyboard) {
                this._keyboard = value;
            }else{
                throw new TypeError('Attempting to pass an object that is not of the expected instance!');
            }
        }

        get monitor(){
            return this._monitor;
        }

        set monitor(value){
            if (value instanceof Monitor) {
                this._monitor = value;
            }else{
                throw new TypeError('Attempting to pass an object that is not of the expected instance!');
            }
        }
    }


    return {
        Battery,
        Keyboard,
        Monitor,
        Computer,
        Laptop,
        Desktop
    }
}



function createMixins() {
    function computerQualityMixin(classToExtend) {
        classToExtend.prototype.getQuality = function () {
            let res = (this.processorSpeed + this.ram + this.hardDiskSpace) / 3;
            return res;
        }

        classToExtend.prototype.isFast = function () {
            if (this.processorSpeed> (this.ram/4)) {
                return true;
            }
            return false;
        }

        classToExtend.prototype.isRoomy = function () {
            return this.hardDiskSpace > Math.floor(this.ram * this.processorSpeed);
        }
    }
    
    function styleMixin(classToExtend) {
        classToExtend.prototype.isFullSet = function () {
            if (this.manufacturer == this.keyboard.manufacturer 
                && this.keyboard.manufacturer == this.monitor.manufacturer) {
                return true;
            }
            return false;
        }

        classToExtend.prototype.isClassy = function () {
            if (this.battery.expectedLife >= 3 && (this.color == 'Silver' || this.color == 'Black') 
            && this.weight< 3) {
                return true;
            }
            return false;
        }
    }


    return {
        computerQualityMixin,
        styleMixin
    }
}
