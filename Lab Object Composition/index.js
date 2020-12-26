// function solution(input) {
//     let res = input
//     .map(([width, height]) => ({
//         width,
//         height,
//         area: function () { return this.width * this.height },
//         compareTo(other) {
//             return other.area() - this.area() || other.width - this.width;
//         }
//     }))
//     .sort((a, b) => a.compareTo(b));

//     return res;
// }

// //solution(([[10,5],[5,12]]));
// console.log(solution([[10, 5], [3, 20], [5, 12]]));


// function solution(input) {
//     let innerCol = [];
//     let commands = {
//         add: (str) => { innerCol.push(str)},
//         remove: (str) => {innerCol = innerCol.filter(x=> x !== str)},
//         print: () => console.log(innerCol.join(','))
//     };

//     for (const command of input) {
//         let [action,value] = command.split(' ');
//          commands[action](value);
//     }
// }

// solution(['add pesho', 'add george', 'add peter', 'remove peter','print']);

// function solution(input) {
//     let inputAsObjs = JSON.parse(input);

//     let res = inputAsObjs.reduce((a,x)=> ({...a,...x}),{});

//    return res;
// }

// solution(`[{"canMove": true},{"canMove":true, "doors": 4},{"capacity": 5}]`); //{ canMove: true, doors: 4, capacity: 5 }

// function solution(input) {
//     let cars = {};
//     let commands= {
//         create: (name) => {let obj = {}; cars[name] = obj;},
//         inherit: (name,parentName) => {let parent = cars[parentName]; cars[name]= {parent:parent}},
//         set: (name,key,value) => {let obj = cars[name]; obj[key]= value},
//         print: (name) => {
//             let obj = cars[name];
//             let arr = [];
//             if (obj.hasOwnProperty('parent')) {
//                 if (obj.parent.hasOwnProperty('parent')) {
//                     let parent = {...obj['parent']['parent']};
//                     arr.push(parent);
//                     delete obj.parent.parent;
//                 }
//                 let parent = {...obj['parent']};
//                 delete obj.parent;
//                 arr.push(parent);
//                 arr.push(obj);
//                 obj = arr.reduce((a,b)=>({...b,...a}));
//             }

//             let output = [];
//             Object.entries(obj).forEach(([key,value])=> {
//                 output.push(`${key}:${value}`);
//             });
//             console.log(output.join(', '));
//         },
//     };

//     for (const command of input) {
//         let [action,name,secondInp, thirdInp] = command.split(' ');
//         if (secondInp==='inherit') {
//             action = secondInp;
//             commands[action](name,thirdInp);
//         }else if (action==='set') {
//             commands[action](name,secondInp,thirdInp);            
//         }
//         else{
//             commands[action](name);
//         }

//     }
// }

// // solution(['create c1',
// // 'create c2 inherit c1',
// // 'set c1 color red',
// // 'set c2 model new',
// // 'print c1',
// // 'print c2']);

// solution(['create pesho',
// 'create gosho inherit pesho',
// 'create stamat inherit gosho',
// 'set pesho rank number1',
// 'set gosho nick goshko',
// 'print stamat']);


console.log(Number(-13) - Number(5));