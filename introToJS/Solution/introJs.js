function stringLength(first, second, third) {
    let sum = first.length + second.length + third.length;

    let average = Math.floor(sum / 3);


    console.log(sum)
    console.log(average);
}



function operations(firstNum, secondNum, operator) {
    let first = parseFloat(firstNum);
    let second = parseFloat(secondNum);

    let res;
    switch (operator) {
        case '+': res = first + second; break;
        case '-': res = first - second; break;
        case '/': res = first / second; break;
        case '*': res = first * second; break;
        case '%': res = first % second; break;
        case '**': res = first ** second; break;
    }

    console.log(res);;
}


function sumOfNandM(n, m) {
    let fistNum = Number(n);
    let secondNum = Number(m);
    let result = 0;

    for (let i = fistNum; i <= secondNum; i++) {
        result += i;
    }


    return result;
}




function largestNumber(n, m, x) {
    return `The largest number is ${Math.max(n, m, x)}.`;
}

function circleArea(input) {
    let type = typeof (input);

    if (type === 'number') {
        let area = Math.pow(input, 2) * Math.PI;
        console.log(Math.round(area * 100) / 100);
    } else {
        console.log(`We can not calculate the circle area, because we receive a ${type}.`);
    }
}


function squareOfStars(size = 5) {
    for (let i = 0; i < size; i++) {
        console.log('* '.repeat(size));
    }
}


function daysOfWeek(input) {
    let res;
    switch (input) {
        case 'Monday': res = 1; break;
        case 'Tuesday': res = 2; break;
        case 'Wednesday': res = 3; break;
        case 'Thursday': res = 4; break;
        case 'Friday': res = 5; break;
        case 'Saturday': res = 6; break;
        case 'Sunday': res = 7; break;
        default: res = 'error';
    }
    return res;
}

function aggregateElement(arrayInput) {
    let sum = arrayInput.reduce((a, b) => a + b, 0);

    let total = 0;
    arrayInput.forEach(n => {
        n = 1 / n;
        total += n;
    });

    let concat = arrayInput.join('');

    console.log(sum);
    console.log(total);
    console.log(concat);;
}



function wordsUppercase(stringInput) {
    let array = stringInput
        .toUpperCase()
        .match(/\w+/g)
        .join(', ');



    console.log(array);
}


function fruit(typeFruit, weight, price) {
    let weigthRounded = (Math.round(weight * 100) / 100) / 1000;
    let moneyRequired = (weigthRounded * price);

    let res = `I need $${moneyRequired.toFixed(2)} to buy ${weigthRounded.toFixed(2)} kilograms ${typeFruit}.`;

    console.log(res);
}


function gcd(n, m) {
    if ((typeof (n) !== 'number') || (typeof (m) !== 'number'))
        return false;

    let x = Math.abs(n);
    let y = Math.abs(m);

    while (y) {
        let t = y;
        y = x % y;
        x = t;
    }

    console.log(x);
}

function sameNumber(input) {
    let numToString = input.toString();
    let isTheSame = function (input) {
        for (let i = 0; i < numToString.length; i++) {
            if (numToString[0] != numToString[i]) {
                return false;
            }
        }
        return true;
    }

    let sum = 0;
    for (let k = 0; k < numToString.length; k++) {
        sum += parseInt(numToString[k]);
    }

    console.log(isTheSame(input));
    console.log(sum);

}

function timeToWalk(steps, footprintInM, speed) {
    let distance = steps * footprintInM;
    let speedMInS = speed / 3.60;

    let restTimes = Math.floor(distance / 500);

    let time = (restTimes * 60) + (distance / speedMInS);

    let totalInHours = Math.floor(time / 3600);
    let totalInMinutes = Math.floor(time / 60);
    let totalInSec = Math.ceil(time % 60);

    function printTime(totalInHours, totalInMinutes, totalInSec) {
        return `${totalInHours < 10 ? 0 : ''}${totalInHours}:${totalInMinutes < 10 ? 0 : ''}${totalInMinutes}:${totalInSec < 10 ? 0 : ''}${totalInSec}`;
    }

    console.log(printTime(totalInHours, totalInMinutes, totalInSec));
}


function roadRadar(inputArr) {
    let speed = Number(inputArr.shift());

    let area = inputArr.shift();

    let limit = 0;
    switch (area) {
        case 'city':
            limit = 50;
            break;
        case 'residential':
            limit = 20;
            break;
        case 'interstate':
            limit = 90;
            break;
        case 'motorway':
            limit = 130;
            break;
    }

    let infraction = '';

    if (speed - limit > 0) {

        if (speed - limit <= 20) {
            infraction = 'speeding';
        }
        else if (speed - limit <= 40) {
            infraction = 'excessive speeding';
        }
        else if (speed - limit > 40) {
            infraction = 'reckless driving';
        }
    }

    console.log(infraction);
}


function cookingByNumber(array) {
    let number = array.shift();

    array.forEach(command => {
        switch (command) {
            case 'chop':
                number /= 2;
                break;
            case 'dice':
                number = Math.sqrt(number);
                break;
            case 'spice':
                number += 1;
                break;
            case 'bake':
                number *= 3;
                break;
            case 'fillet':
                number *= 0.80;
                break;
        }
        console.log(number);
    });
}


function validityChecker(array) {
    let x1 = Number(array.shift());
    let y1 = Number(array.shift());
    let x2 = Number(array.shift());
    let y2 = Number(array.shift());

    console.log(`{${x1}, ${y1}} to {${0}, ${0}} is ${ValidCheck(isValid(x1, y1, 0, 0))}`);
    console.log(`{${x2}, ${y2}} to {${0}, ${0}} is ${ValidCheck(isValid(x2, y2, 0, 0))}`);
    console.log(`{${x1}, ${y1}} to {${x2}, ${y2}} is ${ValidCheck(isValid(x1, y1, x2, y2))}`);

    function isValid(x1, y1, x2, y2) {
        let value = Math.sqrt((x2 - x1) ** 2 + (y2 - y1) ** 2);
        return Number.isInteger(value);
    }

    function ValidCheck(validity) {
        return validity ? 'valid' : 'invalid';
    }
}





function calorieObject(arr) {
    let object={};

    for (let i = 0; i < arr.length; i += 2) {

        let nameOfFood = arr[i];
        let valueInGrams = Number(arr[i + 1]);


       object[nameOfFood] = valueInGrams;
        
    }    
        

        console.log(object);
}

calorieObject(['Yoghurt', '48', 'Rise', '138', 'Apple', '52']);