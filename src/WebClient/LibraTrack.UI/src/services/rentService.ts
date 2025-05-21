export interface Rent{
    id: string;
    userEmail: string;
    bookTitle: string;
    startDate: string;
    endDate: string;
}

export async function getRents() : Promise<Rent[]> {
    try {
        const response = await fetch('https://localhost:7012/api/rents', {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });
    
        if (!response.ok) {
          throw new Error(`Server error: ${response.status}`);
        }
    
        const data: Rent[] = await response.json();
        return data;
      } catch (error) {
        console.error('Error fetching books:', error);
        throw error;
      }
};