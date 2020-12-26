// function solve(arr,startIndex,endIndex) {
//     if (!(arr instanceof Array)) {
//         return NaN;
//     }

//     if (startIndex<0) {
//         startIndex = 0;
//     }

//     if (endIndex > arr.length-1) {
//         endIndex = arr.length;
//     }

//     let finalArr = arr.slice(startIndex,endIndex);
//     let sum = 0;
//     finalArr.forEach(a=>{
//         sum+= Number(a);
//     })

//     return sum;
// }

//console.log(solve([10, 20, 30, 40, 50, 60], 3, 300));
// console.log(solve([10, 'twenty', 30, 40], 0, 2));
// console.log(solve([], 1, 2));
// console.log(solve('text', 0, 2));


// function solve() {
//     class Card {
//         faces = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A'];
//         suits = ['S', 'H', 'D', 'C'];
//         constructor(face, suit) {
//             this.face = face;
//             this.suit = suit;
//         }

//         get face() {
//             return this._face;
//         }
//         set face(value) {
//             if (!this.faces.includes(value.toString())) {
//                 throw new Error('Invalid face!');
//             }

//             this._face = value;
//         }

//         get suit() {
//             return this._suit;
//         }
//         set suit(value) {
//             if (!this.suits.includes(value)) {
//                 throw new Error('Invalid suit!');
//             }
//             this._suit = value;
//         }

//         toString() {
//             let suitAsUTF;
//             if (this.suit === 'S') {
//                 suitAsUTF = '\u2660';
//             } else if (this.suit === 'H') {
//                 suitAsUTF = '\u2665';
//             } else if (this.suit === 'D') {
//                 suitAsUTF = '\u2666';
//             } else if (this.suit === 'C') {
//                 suitAsUTF = '\u2663';
//             }

//             return `${this.face}${suitAsUTF}`;
//         }
//     }

//     return Card;
// }

// // let res = solve();
// // let card = new res(10,'H');
// // console.log(card.toString());


// function printDeckOfCards(cards) {
//     createCard = solve();
    
//     res = [];
//     let isValid = true;
//     cards.forEach(card => {
//         let suit = card[card.length -1];
//         let face = card.slice(0,card.length-1);

//         try {
//             let cardAsUTF = new this.createCard(face,suit);
//             res.push(cardAsUTF.toString());
//         } catch (error) {
//             console.log(`Invalid card: ${card}`);
//             isValid = false;
//         }

//     });

//     isValid===true?console.log(res.join(' ')):'';
// }

// printDeckOfCards(['AS', '10D', 'KH', '2C']);
//printDeckOfCards(['5S', '3D', 'QD', '1C']);

// function sum(arr) {
//     let sum = 0;
//     for (num of arr)
//         sum += Number(num);
//     return sum;
// }

// function isSymmetric(arr) {
//     if (!Array.isArray(arr))
//         return false; // Non-arrays are non-symmetric
//     let reversed = arr.slice(0).reverse(); // Clone and reverse
//     let equal = (JSON.stringify(arr) == JSON.stringify(reversed));
//     return equal;
// }


// function rgbToHexColor(red, green, blue) {
//     if (!Number.isInteger(red) || (red < 0) || (red > 255))
//         return undefined; // Red value is invalid
//     if (!Number.isInteger(green) || (green < 0) || (green > 255))
//         return undefined; // Green value is invalid
//     if (!Number.isInteger(blue) || (blue < 0) || (blue > 255))
//         return undefined; // Blue value is invalid
//     return "#" +
//         ("0" + red.toString(16).toUpperCase()).slice(-2) +
//         ("0" + green.toString(16).toUpperCase()).slice(-2) +
//         ("0" + blue.toString(16).toUpperCase()).slice(-2);
// }

// function createCalculator() {
//     let value = 0;
//     return {
//         add: function(num) { value += Number(num); },
//         subtract: function(num) { value -= Number(num); },
//         get: function() { return value; }
//     }
// }


// module.exports = {
//     sum,
//     isSymmetric,
//     rgbToHexColor,
//     createCalculator
// };