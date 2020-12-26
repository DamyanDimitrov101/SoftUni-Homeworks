function solveClasses() {
    class Article {
        constructor(title, content) {
            // Should have these 2 properties:
            this.title = title;
            this.content = content;
        }

        toString() {
            let res = '';
            // This function should return the title and the content:
            res = `Title: ${this.title}\n`;
            res += `Content: ${this.content}`;
            return res.trim();
        }
    }

    class ShortReports extends Article {
        constructor(title, content, originalResearch) {
            // As we create a short reports here we have a length limit for the content property – it should be less than 150 symbols, otherwise throw an error with the next message:
            if (content.length >= 150) {
                throw new Error('Short reports content should be less then 150 symbols.');
            }
            // The property should have the both required properties , otherwise throw error with this message: 
            // "The original research should have author and title."

            if (!originalResearch.hasOwnProperty('author')
                || !originalResearch.hasOwnProperty('title')) {
                throw new Error('The original research should have author and title.');
            }

            // Should have these 4 properties:
            // •	title – string,  
            // •	content – string, 
            // •	originalResearches – object with properties title and author
            // •	comments – array of strings
            super(title, content);
            this.originalResearch = originalResearch;
            this.comments = [];
        }

        addComment(comment) {
            // This function should receive single comment like string, add it to the comments array and return a message:
            // "The comment is added."
            this.comments.push(comment);
            return 'The comment is added.';
        }

        toString() {
            let res = super.toString();
            // This function should extend the toString method of class Article adding same more lines like:
            res += `\nOriginal Research: ${this.originalResearch.title} by ${this.originalResearch.author}`;
            // And if there are any comments you should print on a new line 
            if (this.comments.length > 0) {
                res += `\nComments:\n`
                res += this.comments.join('\n');
                // "Comments:" 
                // and then all comments each on a new line.
            }

            return res.trim();
        }
    }


    class BookReview extends Article {
        constructor(title, content, book) {
            // Should have these 4 properties:
            super(title, content);

            this.book = book;
            this.clients = [];
            // The client object should have the following structure {clientName, orderDescription}.
        }

        addClient(clientName, orderDescription) {
            // This function should receive clientName and orderDescription as strings. Here you should check our clients array and if we already have this order from the same client throw error with next message: 
            let client = this.clients.find(c => c.clientName === clientName);
            if (client && client.orderDescription === orderDescription) {
                throw new Error('This client has already ordered this review.');
            }

            // Otherwise we add our client object into the clients array and return a message:
            this.clients.push({ clientName, orderDescription });
            return `${clientName} has ordered a review for ${this.book.title}`;
        }

        toString() {
            // This function should extend the toString() method of class Article adding same more lines like:
            let res = super.toString();
            res += `\nBook: ${this.book.name}`;
            // And if there are any orders you should print all orders each on a new line:
            if (this.clients.length > 0) {
                res += `\nOrders:\n`;

                this.clients.forEach(cl=> {
                    res+= `${cl.clientName} - ${cl.orderDescription}.\n`;
                });
            }

            return res.trim();
        }

    }

    return {
        Article,
        ShortReports,
        BookReview
    }
}



let classes = solveClasses();
// let lorem = new classes.Article("Lorem", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce non tortor finibus, facilisis mauris vel…");
// console.log(lorem.toString());
//------------------------------
// let short = new classes.ShortReports("SpaceX and Javascript", "Yes, its damn true.SpaceX in its recent launch Dragon 2 Flight has used a technology based on Chromium and Javascript. What are your views on this ?", { title: "Dragon 2", author: "wikipedia.org" });
// console.log(short.addComment("Thank god they didn't use java."))
// short.addComment("In the end JavaScript's features are executed in C++ — the underlying language.");
// console.log(short.toString()); 
// ------------------------------
let book = new classes.BookReview("The Great Gatsby is so much more than a love story", "The Great Gatsby is in many ways similar to Romeo and Juliet, yet I believe that it is so much more than just a love story. It is also a reflection on the hollowness of a life of leisure. ...", { name: "The Great Gatsby", author: "F Scott Fitzgerald" });
console.log(book.addClient("The Guardian", "100 symbols"));
console.log(book.addClient("Goodreads", "30 symbols"));
console.log(book.toString()); 
