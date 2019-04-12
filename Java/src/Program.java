import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class Program {

	public static void main(String[] args) {
		
		// creating a new instance of Program, to avoid creating static test methods.
		new Program().Start();
	}
	
	// Filepath for the folder that should contain the data files.
    public String FilePath = "C:\\Users\\Anders\\Desktop\\IPC\\Data\\";
    public String FileBaseName = "DataFile";
    public String FileExtension = ".txt";

	public void Start() {
		
		System.out.print(" 1 - Dead Code Elimination \n 2 - Bubble Sort \n => ");
		String input = System.console().readLine();
		
		switch (input) {
			case "1":
				Test1();
				break;
				
			case "2":
				Test2();
				break;
			
			default:
				System.out.println("Invalid command");
				break;
		}
		
		System.out.println("\nEnd...");
	}
	
	void Test1() {
		int count = 0;
        Timer t;

        while (count < 10)
        {
            t = new Timer();
            for (int i = 0; i < 10_000_000; i++)
            {
                double dummy = Multiply(i);
            }
            t.pause();

            count++;
            System.out.printf("\n %.3f", t.check());
        }
	}
	
	int Multiply(int x) {
		
		x += x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x * x;

        return x;
	}
	
	ArrayList<Integer> Test2(){
		
		ArrayList<Integer> dataArray = new ArrayList<>();
		//Integer[] dataArray1 = null;
        boolean dataFileExists = true;
        int fileNr = 1;
        Timer t;

        while (dataFileExists)
        {
            dataArray = FindDataFile(fileNr);
            //dataArray1 = dataArray.toArray(new Integer[dataArray.size()]);

            if (dataArray.size() > 0)
            {
                t = new Timer();
                dataArray = BubbleSort(dataArray);
                t.pause();

                System.out.printf("\nFile nr " + fileNr + "  -  %.3f", t.check());
            }
            else
            {
                dataFileExists = false;
            }
            
            fileNr++;
        }

        return dataArray;
	}
	
	ArrayList<Integer> FindDataFile(int fileNr){
		
		// combines all parts of the file path to an absolute path.
        String fileName = FilePath + FileBaseName + fileNr + FileExtension;
        
        File file = new File(fileName);
        
        if (file.exists())
        {
            ArrayList<Integer> dataArray = new ArrayList<>();
            
            try {
            	
            	BufferedReader reader = new BufferedReader(new FileReader(file));
                String line = reader.readLine();
                
	            while (line != null) {
	            	
	            	
	            	String[] lineAr = line.split(",");
	            	
	            	try {
	            		
	            		int nr = Integer.parseInt(lineAr[0]);
	            		dataArray.add(nr);
	            		
	            	} catch (NumberFormatException e) {
	            		System.out.println("Error in parsing " + lineAr[0] + " to an integer.");
	            	}
	            	
	            	line = reader.readLine();
	            }
                
	            reader.close();
	            
	        } catch (IOException e ) {
	        	
	        	return new ArrayList<>();
	        }
            
            return dataArray;
        } 
        else {
            return new ArrayList<>();
        }
	}
	
	/*
	Integer[] BubbleSort(Integer[] A){
		
		boolean swapped = true;
        int temp, length;
        
        length = A.length;

        while (swapped)
        {
            swapped = false;

            for (int i = 0; i < length-1; i++)
            {
                if (A[i] > A[i+1])
                {
                    temp    =  A[i];
                    A[i]    =  A[i+1];
                    A[i+1]  =  temp;
                    swapped =  true;
                }
            }
        }
        
        return A;
	}*/
	
	
	ArrayList<Integer> BubbleSort(ArrayList<Integer> A){
		
		boolean swapped = true;
        int temp, length;
        
        length = A.size();

        while (swapped)
        {
            swapped = false;

            for (int i = 0; i < length-1; i++)
            {
                if (A.get(i) > A.get(i+1))
                {
                    temp =     A.get(i);
                    A.set(i,   A.get(i+1));
                    A.set(i+1, temp);
                    swapped =  true;
                }
            }
        }
        
        return A;
	}
}

