function solveClasses() {
    class Pet {
        constructor(owner, name) {
            this.owner = owner;
            this.name = name;
            this.comments = [];
        }

        addComment(comment) {
            // This function should receive single comment like string, add it to the comments array and return a message:

            // "Comment is added."
            // If comment is already added to the comments array throw an error with:
            // "This comment is already added!"
            if (this.comments.includes(comment)) {
                throw new Error("This comment is already added!");
            }

            this.comments.push(comment);
            return "Comment is added.";
        }

        feed() {
            // This function should return a simple message:
            return `${this.name} is fed`;
        }

        toString() {
            // This function should return string:
            let res = `Here is ${this.owner}'s pet ${this.name}.`;
            // If there are any comments then add on a new line:
            if (this.comments.length > 0) {
                res += '\n';
                res += `Special requirements: ${this.comments.join(', ')}`;
            }
            // "Special requirements: { comment1 }, { comment2 }, { comment3 ...}"

            return res;
        }

    }

    class Cat extends Pet {
        constructor(owner, name, insideHabits, scratching) {

            super(owner, name);

            this.insideHabits = insideHabits;
            this.scratching = scratching;
        }

        feed() {
            return super.feed() + ", happy and purring.";
            // This function should inherit the feed() method of class Pet and extend the  returned message adding this at the same line at the end:
            // ", happy and purring."
        }

        toString() {

            // This function should extend the toString() method of class Pet, returning the message with some more lines at the end which are:
            let res = super.toString();

            res += "\nMain information:\n";
            res += `${this.name} is a cat with ${this.insideHabits}`;
            // And if scrathing prоperty is true  you should add this at the end: 
            if (this.scratching) {                
                res+=", but beware of scratches.";
            }

            return res;
        }

    }

    class Dog extends Pet{
        constructor(owner, name, runningNeeds, trainability){
            super(owner,name);
            this.runningNeeds = runningNeeds;
            this.trainability = trainability;
            // Should have these 4 properties:
            // •	owner – string,  
            // •	name – string, 
            // •	runningNeeds – string
            // •	trainability – string 
        }

        feed(){
            return super.feed() + ", happy and wagging tail.";
            // This function should inherit the feed() method of class Pet and extend the returned message adding this at the end:
            // ", happy and wagging tail."
        }

        toString(){
            // This function should extend the toString() method of class Pet returning the message with some more lines at the end which are:
            let res = super.toString();
            res+= "\nMain information:\n";
            // "Main information:
            res+=`${this.name} is a dog with need of ${this.runningNeeds}km running every day and ${this.trainability} trainability.`;
            
            return res;
        }
    }

    return {
        Pet,
        Cat,
        Dog
    }
}









let classes = solveClasses();
// let pet = new classes.Pet('Ann', 'Merry');
// console.log(pet.addComment('likes bananas'));
// console.log(pet.addComment('likes sweets'));
// console.log(pet.feed());
// console.log(pet.toString());

// let cat = new classes.Cat('Jim', 'Sherry', 'very good habits', true);
// console.log(cat.addComment('likes to be brushed'));
// console.log(cat.addComment('sleeps a lot'));
// console.log(cat.feed());
// console.log(cat.toString());

let dog = new classes.Dog('Susan', 'Max', 5, 'good');        
console.log(dog.addComment('likes to be brushed'));
console.log(dog.addComment('sleeps a lot'));
console.log(dog.feed());
console.log(dog.toString());
