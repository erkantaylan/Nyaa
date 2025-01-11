// highlight.js
class TorrentHighlighter {
    constructor() {
        this.addHighlightButtons();
    }

    addHighlightButtons() {
        let buttonContainer = document.querySelector('#torrent-controls');
        if (!buttonContainer) {
            buttonContainer = document.createElement('div');
            buttonContainer.id = 'torrent-controls';
            buttonContainer.style.cssText = 'display: flex; gap: 5px; margin-top: 10px;';
            const table = document.querySelector('.torrent-list');
            table.parentNode.insertBefore(buttonContainer, table);
        }

        // Today button
        const todayButton = document.createElement('button');
        todayButton.className = 'btn btn-warning btn-xs';
        todayButton.innerHTML = 'Highlight Today';
        todayButton.onclick = (e) => {
            e.stopPropagation();
            this.clearHighlights();
            this.highlightTodayReleases();
        };

        // Last 24h button
        const last24hButton = document.createElement('button');
        last24hButton.className = 'btn btn-info btn-xs';
        last24hButton.innerHTML = 'Last 24h';
        last24hButton.onclick = (e) => {
            e.stopPropagation();
            this.clearHighlights();
            this.highlightLast24Hours();
        };

        // Clear button
        const clearButton = document.createElement('button');
        clearButton.className = 'btn btn-default btn-xs';
        clearButton.innerHTML = 'Clear';
        clearButton.onclick = (e) => {
            e.stopPropagation();
            this.clearHighlights();
        };

        buttonContainer.appendChild(todayButton);
        buttonContainer.appendChild(last24hButton);
        buttonContainer.appendChild(clearButton);
    }

    clearHighlights() {
        document.querySelectorAll('.today-release, .recent-release').forEach(row => {
            row.classList.remove('today-release', 'recent-release');
            const marker = row.querySelector('.new-marker');
            if (marker) marker.remove();
        });
    }

    highlightTodayReleases() {
        const today = new Date();
        today.setHours(0, 0, 0, 0);

        const rows = document.querySelectorAll('.torrent-list tbody tr');
        rows.forEach(row => {
            const dateCell = row.querySelector('td:nth-child(5)');
            if (!dateCell) return;

            const timestamp = parseInt(dateCell.dataset.timestamp);
            if (!timestamp) return;

            const releaseDate = new Date(timestamp * 1000);
            releaseDate.setHours(0, 0, 0, 0);

            if (releaseDate.getTime() === today.getTime()) {
                this.highlightRow(row, 'today');
            }
        });
    }

    highlightLast24Hours() {
        const now = new Date();
        const twentyFourHoursAgo = new Date(now - 24 * 60 * 60 * 1000);

        const rows = document.querySelectorAll('.torrent-list tbody tr');
        rows.forEach(row => {
            const dateCell = row.querySelector('td:nth-child(5)');
            if (!dateCell) return;

            const timestamp = parseInt(dateCell.dataset.timestamp);
            if (!timestamp) return;

            const releaseDate = new Date(timestamp * 1000);

            if (releaseDate >= twentyFourHoursAgo) {
                this.highlightRow(row, 'recent');
            }
        });
    }

    highlightRow(row, type) {
        // Add appropriate class based on type
        const className = type === 'today' ? 'today-release' : 'recent-release';
        row.classList.add(className);

        // Add marker with different emoji based on type
        const nameCell = row.querySelector('td:nth-child(2)');
        if (nameCell && !nameCell.querySelector('.new-marker')) {
            const marker = document.createElement('span');
            marker.className = 'new-marker';
            marker.innerHTML = type === 'today' ? '✨ ' : '⭐ ';
            marker.style.color = type === 'today' ? '#ff6b6b' : '#2196f3';
            nameCell.insertBefore(marker, nameCell.firstChild);
        }
    }
}

// Initialize highlighter
new TorrentHighlighter();
