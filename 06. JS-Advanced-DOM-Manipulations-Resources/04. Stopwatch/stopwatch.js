function stopwatch() {
    let [startButtonElement, stopButtonElement] = document.getElementsByTagName('button');
    let intervlId;
    
    startButtonElement.addEventListener('click', startTimer);
    stopButtonElement.addEventListener('click', stopTimer);
    

    function startTimer() {
        document.getElementById('time').textContent = '00:00';
        intervlId = setInterval(() => {
            let currentTime = document.getElementById('time');
            let [minutes, seconds] = currentTime.textContent.split(':');
            minutes = Number(minutes);
            seconds = Number(++seconds);

            if (seconds === 60) {
                seconds = 0;
                minutes++;
            }

            if (minutes=== 60) {
                minutes = 0;
            }

            currentTime.textContent = `${minutes < 10 ? '0' + minutes : minutes}:${seconds < 10 ? '0' + seconds : seconds}`;
        }, 1000);

        stopButtonElement.disabled = false;
        startButtonElement.disabled = true;
    }


    function stopTimer() {
        clearInterval(intervlId);
        stopButtonElement.disabled = true;
        startButtonElement.disabled = false;
    }
}
