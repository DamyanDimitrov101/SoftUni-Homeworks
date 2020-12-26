function subtract() {
    let firstElement = document.getElementById('firstNumber').value;
    let secondElement = document.getElementById('secondNumber').value;

    let sumE = firstElement - secondElement;

    let divElementResult = document.getElementById('result');
    divElementResult.innerHTML = sumE;
}