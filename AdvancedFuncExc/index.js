// function solve(arr,str) {
//     let commands = {
//         asc: () => arr.sort((a,b)=> a -b),
//         desc: ()=> arr.sort((a,b)=> b - a),
//     }

//     return commands[str]();
// }

// //console.log(solve([14, 7, 17, 6, 8], 'desc'));

// function solve() {
//     let args=  [...arguments];

//     let types = new Map();
//     args.forEach(a=> {
//         let type = typeof a;
//         if (!types.has(type)) {
//             types.set((type) ,0);
//         }

//         types.set(type,types.get(type)+1);

//         console.log(`${type}: ${a}`);
//     });


//     [...types.entries()]
//     .sort((a,b)=> b[1]-a[1])
//     .forEach(t=> {
//         let [type,count] = t;
//         console.log(`${type} = ${count}`);
//     });
// }

// solve('cat', 42,function () { console.log('Hello world!')}, 45);
// console.log(`-`.repeat(40));
// solve( { name: 'bob'}, { name: 'pesho'}, 3.333, 9.999, 'dog');

// function solve() {
//     let [name,age,weight,height] = arguments;
//     let bmi = Math.round((weight/(height**2))*10000);
//     function calcStatus(bmi) {
//         if (bmi<18.5) {
//             return 'underweight';
//         }else if(bmi<25){
//             return 'normal';
//         }else if (bmi<30) {
//             return 'overweight';
//         }else{
//             return 'obese';
//         }
//     }

//     let person = {
//         name,
//         personalInfo:{
//             age,
//             weight,
//             height
//         },
//         BMI: bmi,
//         status: calcStatus(bmi),
//     };

//     if (person.status==='obese') {
//         person.recommendation = 'admission required';
//     }

//     return person;
// }

//console.log(solve('Peter', 29, 75, 182));
//console.log(solve('Honey Boo Boo', 9, 57, 137));


// (function solution() {
//     return result = {
//         add: (vec1, vec2) => [+vec1[0] + (+vec2[0]), +vec1[1] + (+vec2[1])],
//         multiply: (vec1, scalar) => [(+vec1[0]) * (+scalar), (+vec1[1]) * (+scalar)],
//         length: (vec1) => Math.sqrt(+vec1[0] * (+vec1[0]) + (+vec1[1]) * (+vec1[1])),
//         dot: (vec1, vec2) => (+vec1[0] * (+vec2[0]) + (+vec1[1]) * (+vec2[1])),
//         cross: (vec1, vec2) => ((+vec1[0] * (+vec2[1])) - (+vec1[1] * (+vec2[0]))),
//     };  
// }())


// console.log();
// console.log(solution.multiply([3.5, -2], 2));//	[7, -4]	[3.5 * 2, (-2) * 2] = [7, -4]
// console.log(solution.length([3, -4]));//	5	sqrt(3 * 3 + (-4) * (-4)) = 5
// console.log(solution.dot([1, 0], [0, -1]));//	0	1 * 0 + 0 * (-1) = 0
// console.log(solution.cross([3, 7], [1, 0]));//	-7	3 * 0 – 7 * 1 = -7


// function result() {

//     let recipes = {
//         apple: {carbohydrate: 1,flavour: 2 },
//         lemonade: {carbohydrate: 10 ,flavour:20},
//         burger: {carbohydrate:5, fat:7, flavour:3 },
//         eggs: {protein:5 ,fat: 1, flavour: 1}, 
//         turkey:{protein: 10, carbohydrate:10 , fat :10 ,flavour:10}
//     };

//     let ingridients = {
//         protein: 0, 
//         carbohydrate: 0, 
//         fat: 0,
//         flavour: 0,
//     };

//     let commands = {
//         restock: (microelement, quantity)=> {
//             ingridients[microelement] += quantity;
//             return 'Success';
//         },
//         prepare: (recipe,quantity) => {
//             let recept = Object.entries(recipes[recipe]);

//             for (const [micro,value] of recept) {   
//                 if (ingridients[micro]< value*quantity) {
//                     return `Error: not enough ${micro} in stock`;
//                 }
//             }

//                 recept.forEach(([item,value])=> {
//                     ingridients[item]-= value*quantity;
//                 });

//                 return 'Success';

//         },
//         report: () => {
//             let res= Object.entries(ingridients)
//             .map(([item,value])=> `${item}=${value}`)
//             .join(' ');
//             return res;
//         }
//     };

//     return (input) => {
//         let [command, item, quantity]= input.split(' ');
//         let action = commands[command];
//         return action(item,+quantity);
//     }

// }

// let manager = solution();
// manager("prepare turkey 1"); 
// manager("restock protein 10");
// manager("prepare turkey 1"); 
// manager('restock carbohydrate 10');
// manager("prepare turkey 1"); 
// manager('restock fat 10');
// manager("prepare turkey 1"); 
// manager('restock flavour 50');
// manager("prepare turkey 1"); 
// manager('report');



//  console.log(add(1)(6)(-3));

// solution = (()=>{
//     let commands= {

//     };

//     return {call: (post,command)=> commands[command](post)};
// })();


