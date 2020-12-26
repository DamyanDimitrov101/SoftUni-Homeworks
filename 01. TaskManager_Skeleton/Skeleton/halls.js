function solveClasses() {
    class Hall{
        constructor(capacity, name){
            // Should have these 3 properties:
            this.capacity =Number(capacity); 
            this.name =name;
            this.events = [];   
        }
        
        hallEvent(title) {
            // If event is already added to the events array throw an error with:
            let event = this.events.find(e=> e===title);
            if (event) {
                throw new Error('This event is already added!');
            }
            // This function should receive single title like string, add it to the events array and return a message:
            this.events.push(title);
            return "Event is added.";            
        }
            close(){
                // This function should clear the events array and return a simple message:
                this.events= [];
                return `${this.name} hall is closed.`;
            }
            toString(){
                // This function should return:
                let res =`${this.name} hall - ${this.capacity}`;
                // If there are any events then add on a new line:
                if (this.events.length>0) {
                    res+=`\nEvents: ${this.events.join(', ')}`;
                }

                return res.trim();
            }
    }

    class MovieTheater extends Hall{
        constructor( capacity, name, screenSize ){
            // Should have these 4 properties:
            super(Number(capacity),name);
            super.events = [];   
            this.screenSize = screenSize;
        }
        
        close(){
            // This function should inherit the close() method of class Hall and extend the  returned message adding this at the end on the same line:
            let res = super.close();
            res+="Аll screenings are over.";
            
            return res.trim();
        }
        
        toString(){
            // This function should extend the toString() method of class Hall returning the message with next string on a new line:
            let res = super.toString();
            res+=`\n${this.name} is a movie theater with ${this.screenSize} screensize and ${this.capacity} seats capacity.`;

            return res.trim();
        }

    }

    class ConcertHall extends Hall{
        constructor( capacity, name ){
            // Should have these 3 properties:
            super(Number(capacity),name);
            super.events = [];
            this.performers = [];  
        }
        hallEvent( title, performers ) {
            // This function should receive single title like string and performers – array of strings. This method inherit the hallEvents() method of class Hall like adding title to the events array, but also creates new property performers and returns a message:
            let res = super.hallEvent(title);
            Array.from(performers).forEach(performer=> {
                this.performers.push(performer);                
            });
            // "Event is added."
            // If event is already added to the events array throw an error with:
            // "This event is already added!"
            return res.trim();
        }
        close(){
            // This function should inherit the close() method of class Hall and extend the  returned message adding this at the end on the same line:
            let res = super.close();
            res+=`Аll performances are over.`;
            return res.trim();
        }
        
        toString(){
            // This function should extend the toString() method of class Hall. If there are any events into the events array, add to the returned message on new line:
            let res = super.toString();

            if (this.events.length>0) {
                res+=`\nPerformers: ${this.performers.join(', ')}.`;                
            }

            return res.trim();
        }
        
    }

    return {
        Hall,
        MovieTheater,
        ConcertHall
    }
}


let classes = solveClasses();
let hall = new classes.Hall(20, 'Main');
console.log(hall.hallEvent('Breakfast Ideas'));
console.log(hall.hallEvent('Annual Charity Ball'));
console.log(hall.toString());
console.log(hall.close()); 
//--------------------------------------------------------------------------------------
let movieHall = new classes.MovieTheater(10, 'Europe', '10m');
console.log(movieHall.hallEvent('Top Gun: Maverick')); 
console.log(movieHall.toString());
//--------------------------------------------------------------------------------------
let concert = new classes.ConcertHall(5000, 'Diamond');        
console.log(concert.hallEvent('The Chromatica Ball', ['LADY GAGA','LADY GAGA']));  
console.log(concert.toString());
console.log(concert.close());
console.log(concert.toString());
