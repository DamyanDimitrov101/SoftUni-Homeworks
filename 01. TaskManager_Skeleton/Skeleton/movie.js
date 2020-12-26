class Movie {
    constructor ( movieName, ticketPrice ){
        this.movieName = movieName;
        this.ticketPrice = Number(ticketPrice);
        this.screenings = [];
        
        this.totalSoldMovieTickets = 0;
        this.totalProfit = 0;
        // Receives 2 parameters at initialization of the class - movieName and ticketPrice.  
        // Should have these 3 properties:
        // ·	movieName – property of type string;
        // ·	ticketPrice – property of type number, may come as a string;
        // ·	screenings– initially an empty array;
        // Hint: Here you can add more properties to help you finish the task.  
    }

    newScreening(date, hall, description){
        // The date, hall and description are of type string. 
        // •	Check if there is already entered screening with the same date and hall and  throw an Error:
        let screening = this.screenings.find(s=>s.date===date&&s.hall===hall);
        if (screening) {
            throw new Error(`Sorry, ${hall} hall is not available on ${date}`);   
        }
        // •	Otherwise this function should add the screening to the screenings array and return:
        screening = {date,hall,description};
        this.screenings.push(screening);
        return `New screening of ${this.movieName} is added.`;
    }

    endScreening(date, hall, soldTickets) {
        // ·	Check if the given screening array has a screening with the given date and hall, if NOT throw an Error:
        let screening = this.screenings.find(s=> s.date===date&&s.hall===hall);

        if (!screening) {
            throw new Error(`Sorry, there is no such screening for ${this.movieName} movie.`);
        }
        // •	Otherwise  calculate the current screening profit of sold tickets, add the profit to the total profit for the movie, also add the sold tickets count to the total sold movie tickets, delete the screening from the screenings array and return:        
        
        let currentScreeningProfit = soldTickets* this.ticketPrice;
        this.totalProfit+= currentScreeningProfit;
        this.totalSoldMovieTickets+= soldTickets;

        this.screenings.splice(this.screenings.indexOf(screening),1);
        
        return `${this.movieName} movie screening on ${screening.date} in ${screening.hall} hall has ended. Screening profit: ${currentScreeningProfit}`;
    }
        
    toString (){
        // In the end is the toString() method where we return the full information of the movie.
        // ·	At the first line:
        let res = `${this.movieName} full information:`;
        // •	On the second two lines comes the collected profit, fixed to 0 and count of all sold tickets: 
        res+= `\nTotal profit: ${this.totalProfit.toFixed(0)}$`;
        res+= `\nSold Tickets: ${this.totalSoldMovieTickets}`;
        // •	If there are screenings into the screening array, add on new line:
            if (this.screenings.length>0) {
                res+= `\nRemaining film screenings:\n`;
                res+= Array.from(this.screenings).sort((a,b)=> a.hall.localeCompare(b.hall)).map(({date,hall,description})=> `${hall} - ${date} - ${description}`).join('\n');
            }else{
                res+= "\nNo more screenings!";
            }

            return res.trim();
        // ""
        //  Sort screenings alphabetically by hall name and add a message for each of them on new line:
        // "{hall} - {date} - {desrtiption}"
        // •	If there are no screenings into screenings array add this line to the returned message:
        // "No more screenings!"
    }

}


let m = new Movie('Wonder Woman 1984', '10.00');
console.log(m.newScreening('October 2, 2020', 'IMAX 3D', `3D`));
console.log(m.newScreening('October 3, 2020', 'Main', `regular`));
console.log(m.newScreening('October 4, 2020', 'IMAX 3D', `3D`));
console.log(m.endScreening('October 2, 2020', 'IMAX 3D', 150));
console.log(m.endScreening('October 3, 2020', 'Main', 78));
console.log(m.toString());

m.newScreening('October 4, 2020', '235', `regular`);
m.newScreening('October 5, 2020', 'Main', `regular`);
m.newScreening('October 3, 2020', '235', `regular`);
m.newScreening('October 4, 2020', 'Main', `regular`);
console.log(m.toString());
