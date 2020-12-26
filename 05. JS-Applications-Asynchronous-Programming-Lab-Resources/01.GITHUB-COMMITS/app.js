function loadCommits() {
    const inputUsername = document.getElementById('username').value;
    const inputRepo = document.getElementById('repo').value;
    const commitsElement = document.getElementById('commits');

    const url = `https://api.github.com/repos/${inputUsername}/${inputRepo}/commits`;

    let responseStatus;
    let responseStatusText;

    fetch(url)
        .then(response => {
            responseStatus = response.status;
            responseStatusText = response.statusText;

            return response;
        })
        .then(response => response.json())
        .then(data => {
            commitsElement.innerHTML = Array.from(Object.keys(data)).map(c => `<li>${data[c].commit.author.name}: ${data[c].commit.message}</li>`).join('');
        })
        .catch(e => {
            commitsElement.innerHTML = `<li>Error: ${responseStatus} (${responseStatusText})</li>`;
        })
}