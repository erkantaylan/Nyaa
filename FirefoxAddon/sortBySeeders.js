document.body.style.border = "5px solid red";

let sortDirection = 'desc';

function sortTorrentsBySeeder() {
    const tbody = document.querySelector('.torrent-list tbody');
    const rows = Array.from(tbody.querySelectorAll('tr'));

    rows.sort((a, b) => {
        const seedersA = parseInt(a.querySelector('td:nth-child(6)').textContent);
        const seedersB = parseInt(b.querySelector('td:nth-child(6)').textContent);
        return sortDirection === 'desc' ? seedersB - seedersA : seedersA - seedersB;
    });

    sortDirection = sortDirection === 'desc' ? 'asc' : 'desc';
    rows.forEach(row => tbody.appendChild(row));
}

// Add a sort button to the table header
function addSortButton() {
    // Option 1: Add next to the existing header cell for seeders
    const seederHeader = document.querySelector('.hdr-seeders');
    if (!seederHeader) return;

    const sortButton = document.createElement('button');
    sortButton.className = 'btn btn-primary btn-xs';
    sortButton.style.cssText = 'margin-left: 5px; vertical-align: middle;';
    sortButton.innerHTML = 'Sort ↓';

    // Clear existing content and add our button
    seederHeader.innerHTML = '<i class="fa fa-arrow-up" aria-hidden="true"></i>';
    seederHeader.appendChild(sortButton);

    let sortDirection = 'desc';
    sortButton.onclick = (e) => {
        // Prevent the click from bubbling to parent elements
        e.stopPropagation();

        sortTorrentsBySeeder();
        sortButton.innerHTML = `Sort ${sortDirection === 'desc' ? '↓' : '↑'}`;
        sortDirection = sortDirection === 'desc' ? 'asc' : 'desc';
    };
}

// Initialize
addSortButton();