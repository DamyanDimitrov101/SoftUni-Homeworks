function solve() {
    let numberInputElement = document.getElementById('input');
    let selectToMenuElement = document.getElementById('selectMenuTo');

    let optionBinaryElement = document.createElement('option');
    let optionHexadeicmalElement = document.createElement('option');


    optionBinaryElement.textContent = 'Binary';
    optionHexadeicmalElement.textContent = 'Hexadecimal';

    optionBinaryElement.value = 'binary';
    optionHexadeicmalElement.value = 'hexadecimal';


    let child = document.querySelector('#selectMenuTo option');
    child.remove();
    selectToMenuElement.appendChild(optionBinaryElement);
    selectToMenuElement.appendChild(optionHexadeicmalElement);

    let result = document.querySelector('#result');

    let buttonElement = document.querySelector('#container button');
    buttonElement.addEventListener('click', onClick);

    function onClick() {
        if (selectToMenuElement.value === 'binary') {
            result.value =decimalToBinary(+numberInputElement.value);

        } else if (selectToMenuElement.value === 'hexadecimal') {
            result.value = decimalToHexString(+numberInputElement.value);
        }

        numberInputElement.value = '';
    }

    function decimalToHexString(number) {
        if (number < 0) {
            number = 0xFFFFFFFF + number + 1;
        }

        return (number).toString(16).toUpperCase();
    }

    function decimalToBinary(x) {
        let bin = 0;
        let rem, i = 1, step = 1;
        while (x != 0) {
            rem = x % 2;
            x = parseInt(x / 2);
            bin = bin + rem * i;
            i = i * 10;
        }
        return bin;
    }
}