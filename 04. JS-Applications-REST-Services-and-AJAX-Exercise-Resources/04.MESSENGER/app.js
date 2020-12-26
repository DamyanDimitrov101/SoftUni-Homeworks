function attachEvents() {
    const refreshBtn = document.getElementById('refresh');
    const sendBtn = document.getElementById('submit');
    const messagesAreaElement = document.getElementById('messages');

    let url = 'https://rest-messanger.firebaseio.com/messanger.json';

    refreshBtn.addEventListener('click', () => {
        fetch(url)
            .then(response => response.json())
            .then(data => {
                let tempStr = '';
                
                Array.from(Object.keys(data))
                    .forEach((m) => {
                    let text = `${data[m].author}: ${data[m].content}\n`;
                       
                    tempStr+= text;
                })

                messagesAreaElement.textContent = tempStr.trim();
            });
    })

    sendBtn.addEventListener('click', ()=>{
        const authorElementInput = document.getElementById('author');
        const contentElementInput = document.getElementById('content');
        let message = {author:authorElementInput.value, content: contentElementInput.value};

        fetch(url, {method: "Post", body: JSON.stringify(message)});
        authorElementInput.value = '';
        contentElementInput.value = '';
    })
}

attachEvents();