function shortenDescription(description) {
    return description.length > 40 ? description.substring(0, 40) + "..." : description;
}

function showHasEnded(hasEnded) {
    return hasEnded ? "Yep" : "Nope";
}