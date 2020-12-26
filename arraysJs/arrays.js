function sumFirstLast(arr) {
    let first = Number(arr[0]);
    let second = Number(arr[arr.length - 1]);

    console.log(first + second);
}

function evenPositionElement(arr) {
    let res = [];
    for (let i = 0; i < arr.length; i += 2) {
        res.push(arr[i]);
    }

    console.log(res.join(' '));
}


function negativePositiveNumbers(arr) {
    let res = [];
    for (el of arr) {
        if (el < 0) {
            res.unshift(el);
        } else {
            res.push(el);
        }
    }

    res.forEach(x => {
        console.log(x);
    });
}

function lastKNumbersSequence(n, k) {
    let arr = [1];
    let counter = k;
    for (let i = 0; i < n - 1; i++) {
        let toSlice = i < k ? 0 : (i + 1) - k;
        let arrToCalc = arr.slice(toSlice, counter);
        arr.push(arrToCalc.reduce((a, x) => a + x, 0));
        counter++;
    }

    console.log(arr.join(' '));
}

function processOddNumbers(arr) {
    let nums = arr
        .filter((x, i) => i % 2 != 0)
        .map(x => x * 2)
        .reverse();

    console.log(nums.join(' '));
}


function smallestTwoNumbers(arr) {
    function comparer(a, b) {
        return a - b;
    }

    let nums = arr
        .sort(comparer)
        .slice(0, 2);

    console.log(nums.join(' '));
}


function biggestElement(jaggedArr) {
    let biggest = Number.MIN_SAFE_INTEGER;
    jaggedArr.forEach(x => {
        let max = Math.max(...x);
        if (max > biggest) {
            biggest = max;
        }
    });

    console.log(biggest);
}


function diagonalSums(matrix) {
    let sumFirst = 0;
    let sumSecond = 0;

    let startRowFirst = 0;
    let startColFirst = 0;

    let startRowSecond = 0;
    let startColSecond = matrix[0].length - 1;

    for (let row = 0; row < matrix.length; row++) {


        let elementFirst = matrix[startRowFirst][startColFirst];
        let elementSecond = matrix[startRowSecond][startColSecond];

        sumFirst += elementFirst;
        sumSecond += elementSecond;

        startRowFirst++;
        startColFirst++;

        startRowSecond++;
        startColSecond--;
    }

    let concat = sumFirst + ' ' + sumSecond;
    console.log(concat);
}


function equalNeighbors(matrix) {
    let pairs = 0;

    for (let row = 0; row < matrix.length; row++) {
        for (let col = 0; col < matrix[row].length; col++) {
            let element = matrix[row][col];





            if (row < matrix.length - 1) {
                if (element == matrix[row + 1][col]) {
                    pairs++;
                }
            }




            if (col < matrix[row].length) {

                if (element == matrix[row][col + 1]) {
                    pairs++;
                }
            }
        }
    }
    console.log(pairs);
}



function printAnArrayWithAGivenDelimiter(arr) {
    let delimeter = arr.pop();

    console.log(arr.join(delimeter));
}


function PrintEveryNthElementfromanArray(arr) {
    let step = Number(arr.pop());
    arr.forEach((el, i) => {
        if (i % step == 0) {
            console.log(el);
        }
    });
}



function addandRemoveElements(arr) {
    let num = 1;
    let res = [];
    arr.forEach((el) => {
        el === 'add' ? res.push(num) : res.pop();

        num++;
    });

    console.log(res.length > 0 ? res.join('\n') : 'Empty');
}



function rotateArray(arr) {
    let rotations = arr.pop();

    for (let i = 0; i < rotations % arr.length; i++) {

        arr.unshift(arr.pop());
    }

    console.log(arr.join(' '));
}


function extractIncreasingSubsequenceFromArray(arr) {
    let biggest = Number.MIN_SAFE_INTEGER;
    let result = [];

    arr.forEach(el => {
        if (el >= biggest) {
            biggest = el;
            result.push(el);
        }
    });

    console.log(result.join('\n'));
}


function sortAnArrayBy2Criteria(arr) {

    let comparer = (a, b) => a.length - b.length || a.localeCompare(b);

    arr = arr.sort(comparer);


    console.log(arr.join('\n'));
}


function magicMatrices(arr) {
    let isSame = false;

    let sum = arr[0].reduce((a, x) => a + x, 0);

    for (let i = 0; i < arr.length; i++) {
        let rowSum = arr[i].reduce((a, x) => a + x, 0);

        if (rowSum === sum) {
            isSame = true;
        } else {
            isSame = false;
            break;
        }
    }

    for (let j = 0; j < arr[0].length; j++) {
        let colSum = arr.reduce((acc, x) => acc + x[j], 0);

        if (colSum === sum) {
            isSame = true;
        } else {
            isSame = false;
            break;
        }
    }

    console.log(isSame);
}



function ticTacToe(arr) {
    let dashboard = [[false, false, false],
    [false, false, false],
    [false, false, false]];

    let player = 'X';

    for (let i = 0; i < arr.length; i++) {
        let turn = arr[i];
        let [row, col] = turn.split(' ');

        if (dashboard[row][col] === 'O' || dashboard[row][col] === 'X') {
            console.log("This place is already taken. Please choose another!");
            continue;
        } else {
            dashboard[row][col] = player;
        }


        if (checkIfWins() == true) {
            console.log(`Player ${player} wins!`);
            printMatrix(dashboard);
            return;
        }


        let isFalse = false;
        dashboard.forEach(row => {
            row.forEach(col => {
                if (col === false) {
                    isFalse = true;
                }
            });
        });
        if (isFalse === false) {
            console.log("The game ended! Nobody wins :(");
            break;
        }


        player = player === 'X' ? 'O' : 'X';
    }




    printMatrix(dashboard);



    function checkIfWins() {
        let isWinner = false;
        for (let i = 0; i < dashboard.length; i++) {
            let row = dashboard[i][0] === player && dashboard[i][1] === player && dashboard[i][2] === player ? true : false;

            let col = dashboard[0][i] === player && dashboard[1][i] === player && dashboard[2][i] === player ? true : false;

            if (col == true || row == true) {
                isWinner = true;
                break;
            }
        }

        let diagonalLeft = dashboard[0][0] === player && dashboard[1][1] === player && dashboard[2][2] === player;
        let diagonalRigth = dashboard[2][0] === player && dashboard[1][1] === player && dashboard[0][2] === player;

        if (isWinner) {
            return true;
        } else if (diagonalLeft == true || diagonalRigth == true) {
            return true;
        } else {
            return false;
        }
    }

    function printMatrix(dashboard) {
        for (let i = 0; i < dashboard.length; i++) {
            console.log(dashboard[i].join('\t'));
        }
    }
}

// ticTacToe(["0 1",
//             "0 0",
//             "0 2", 
//             "2 0",
//             "1 0",
//             "1 1",
//             "1 2",
//             "2 2",
//             "2 1",
//             "0 0"]
//             );



// ticTacToe(["0 0",
// "0 0",
// "1 1",
// "0 1",
// "1 2",
// "0 2",
// "2 2",
// "1 2",
// "2 2",
// "2 1"]
// );


// ticTacToe(["0 1",
//     "0 0",
//     "0 2",
//     "2 0",
//     "1 0",
//     "1 2",
//     "1 1",
//     "2 1",
//     "2 2",
//     "0 0"]
// );


function diagonalAttack(arr) {
    let matrix=[];

    arr.forEach(row => {
        let rowToArr = row.split(' ').map(x=>Number(x));
        matrix.push(rowToArr);
    })

    let diagoalIndexRow =0;
    let diagonalIndexCol = 0;
    let diaFirstSum =0;

    let diagoalIndexRowSecond =0;
    let diagonalIndexColSecond = matrix[0].length-1;
    let diaSumSecond =0;
    matrix.forEach((row,index) => {
        
        diaFirstSum += matrix[diagoalIndexRow][diagonalIndexCol];
            
        diaSumSecond+= matrix[diagoalIndexRowSecond][diagonalIndexColSecond];
       
        diagoalIndexRow++;
        diagonalIndexCol++;

        diagoalIndexRowSecond++;
        diagonalIndexColSecond--;
    });


     diagoalIndexRow =0;
     diagonalIndexCol = 0;
    
     diagoalIndexRowSecond =0;
     diagonalIndexColSecond = matrix[0].length-1;
    
    if (diaFirstSum!==diaSumSecond) {
        matrix.forEach(row => {
            console.log(row.join(' '));
        });
    }else{
        let res=[];
        matrix.forEach((row,indexRow) => {
            
            let rowRes = row.map((x,i)=>x=(i!==diagonalIndexCol)&&(i!==diagonalIndexColSecond)?diaFirstSum:x);
          
            res.push(rowRes);

            diagoalIndexRow++;
            diagonalIndexCol++;

            diagoalIndexRowSecond++;
            diagonalIndexColSecond--;
        });

        res.forEach(row => {
            console.log(row.join(' '));
        });
    }
}

diagonalAttack(['5 3 12 3 1',
                '11 4 23 2 5',
                '101 12 3 21 10',
                '1 4 5 2 2',
                '5 22 33 11 1']
);