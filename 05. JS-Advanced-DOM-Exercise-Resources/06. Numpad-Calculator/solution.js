function solve() {
    let output= document.querySelector('p#expressionOutput');

    let result = document.getElementById('resultOutput');

    let buttonsElements = document.getElementsByClassName('keys')[0];
    buttonsElements.addEventListener('click',symbolClicked);

    let clearButtonElement = document.querySelector('.clear');
    clearButtonElement.addEventListener('click',clear);


    function  clear() {
        output.textContent='';
        result.textContent = '';
    }


    function symbolClicked(e) {
        let symbol = e.target.value;

        switch (symbol) {
            case '+':
            case '-':
            case '*':
            case '/':
                output.textContent+=` ${symbol} `;
            break;
            case '=':
               calculate();
                break;
            default:
                output.textContent+=symbol;
                break;
        }
    }


    function calculate() {
        let [leftOperand,operator,rigthOperand] = output.textContent.split(' ');
        
        console.log(leftOperand);
        console.log(operator);
        console.log(rigthOperand);

        if (leftOperand==''||rigthOperand==''||operator=='') {
            result.textContent = 'NaN';
        }else{
            switch (operator) {
                case '+':
                    result.textContent = (+leftOperand) + (+rigthOperand);
                    break;
                case '-':
                        result.textContent = (+leftOperand) - (+rigthOperand);
                        break;
                case '*':
                    result.textContent = (+leftOperand) * (+rigthOperand);
                    break;
                case '/':
                            result.textContent = (+leftOperand) / (+rigthOperand);
                            break;
            }
        }
    }
}