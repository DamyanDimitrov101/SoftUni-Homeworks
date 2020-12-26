function attachEventsListeners() {
    let mainElement = document.getElementsByTagName('main')[0];
    let inputsElements = [...document.getElementsByTagName('input')].filter(x=>x.type==='text');

    let ratio = {
        'days':1,
        'hours':24,
        'minutes': 1440,
        'seconds':86400
    }

    function calc(value,form) {
        let inDays = Number(value)/ratio[form];
        return {
            days : inDays,
            hours: inDays*ratio['hours'],
            minutes : inDays*ratio['minutes'],
            seconds : inDays * ratio['seconds']
        };
    }

    mainElement.addEventListener('click',onClick);
    
    function onClick(e) {
        if (e.target.type === 'button') {
            let input = e.target.previousElementSibling.value.trim();
            let form = e.target.previousElementSibling.id;
            console.log(input);
            let res = calc(input,form);

            setToTextArea(res);
        }
    }

    function setToTextArea(res) {
        inputsElements[0].value = res.days;
        inputsElements[1].value = res.hours;
        inputsElements[2].value = res.minutes;
        inputsElements[3].value = res.seconds;
    }
}