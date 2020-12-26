// function solution(num) {
//     function add(numToAdd) {
//         let  res =numToAdd+num;
//         return res;
//     }
//     return add;
// }

// let add5 = solution(5);
// console.log(add5(2));
// console.log(add5(3));


function result(f) {
    let resFunc = f.bind(null,",","$",true);
    return resFunc;
}


function currencyFormatter(separator, symbol, symbolFirst, value) {
    let result = Math.trunc(value) + separator;
    result += value.toFixed(2).substr(-2,2);
    if (symbolFirst) return symbol + ' ' + result;
    else return result + ' ' + symbol;
}

// let dollarFormatter = result(currencyFormatter);
// console.log(dollarFormatter(5345));   // $ 5345,00
// console.log(dollarFormatter(3.1429)); // $ 3,14
// console.log(dollarFormatter(2.709));  // $ 2,71

function filterEmp(data,criteria) {
    let empsObj = JSON.parse(data);
    let counter= 0;
    if (criteria==='all') {
        [...empsObj.values()].forEach(emp => {
            console.log(`${counter++}. ${emp.first_name} ${emp.last_name} - ${emp.email}`);
        });
    }else{
        let [param, valueToEq] = criteria.split('-');

        let filteredEmp = [...empsObj.values()].filter(x=> x[param] === valueToEq);

        [...filteredEmp.values()].forEach(emp => {
            console.log(`${counter++}. ${emp.first_name} ${emp.last_name} - ${emp.email}`);
        });
    }
}


// let res = filterEmp(
//     `[{
//         "id": "1",
//         "first_name": "Kaylee",
//         "last_name": "Johnson",
//         "email": "k0@cnn.com",
//         "gender": "Female"
//       }, {
//         "id": "2",
//         "first_name": "Kizzee",
//         "last_name": "Johnson",
//         "email": "kjost1@forbes.com",
//         "gender": "Female"
//       }, {
//         "id": "3",
//         "first_name": "Evanne",
//         "last_name": "Maldin",
//         "email": "emaldin2@hostgator.com",
//         "gender": "Male"
//       }, {
//         "id": "4",
//         "first_name": "Evanne",
//         "last_name": "Johnson",
//         "email": "ev2@hostgator.com",
//         "gender": "Male"
//       }]`,
//      'last_name-Johnson'
//        );


function solution() {
    let stringToKeep = '';

    return {
        append: string=> stringToKeep += string,
    
        removeStart: n => stringToKeep = stringToKeep.substr(n),
    
        removeEnd: n=>stringToKeep = stringToKeep.slice(0,stringToKeep.length-n),
    
        print: () =>console.log(stringToKeep)
    };
}

let firstZeroTest = solution();

firstZeroTest.append('hello');
firstZeroTest.append('again');
firstZeroTest.removeStart(3);
firstZeroTest.removeEnd(4);
firstZeroTest.print();