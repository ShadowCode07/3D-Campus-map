const API_BASE = "https://campusmapapi-fkb7azh6h9hah4b9.germanywestcentral-01.azurewebsites.net/api";

async function LoadScenes() {
  try{
    let url = `${API_BASE}/scenes`;

    const response = await fetch(url);

    if (!response.ok) {
      throw new Error("Failed to fetch scenes");
    }

    const scenes = await response.json();
    return scenes;
  } catch (error) {
    console.error("Error loading scenes:", error);
  }
}