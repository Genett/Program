using System;
using System.Collections.Generic;

namespace Simulation
{
    class Cell
    {
        private Random random = new Random();

        private int initial_dnaLength = 10000;  //This variable indicates how long will be the first(s) dna string(s)
        private double mutationRate = 0.1;  //This is how often mutations will occur. It's indicated in percentage

        private string[] dnaNitrogenousBase = {"A","T","C","G"};  //These are the nitrogenous bases that will form the string dna
        
        private List<string> genome = new List<string>(); //This list contains all the genes of the cell
        private string[] protein;  //This array contains the proteins a cell can produce
        private string[] mrna;  //This array contains the mRNA's necessaries to produce the proteins
        private string dna = ""; //This is the DNA of the cell

        

        public Cell() {   //This constructor should be used to create a new cell with random DNA
            for(int i = 0; i < initial_dnaLength; i++){
                dna += dnaNitrogenousBase[random.Next(4)];
            }

            setCellProperties(); //This method sets the rest of the cell's properties
        }

        
        
        
        public Cell(string parent_dna) {  //This constructor should be used to create a cell wich inherits DNS from its  parent
            for (int i = 0; i < parent_dna.Length; i++) { //It will loop throught all the parent's DNA string
                if(The_Universe_Wants_a_Mutation_Here() )  //Ok, this was a joke. But check out what this method does anyway :)
                    switch(random.Next(3)){ //In case the universe wants it, there are three possible mutations:
                        case 0:    //Do nothing. In other words, delete the nitrogenous base. It makes the DNA string shorter
                            break;
                        case 1:    //Replace the nitrogenous base for another. It doesn't affect the DNA string's length
                            dna += dnaNitrogenousBase[random.Next(4)];
                            break;
                        case 2:   //Add a new random nitrogenous base keeping the parent's. It makes the DNA string larger
                            dna += dnaNitrogenousBase[random.Next(4)];
                            dna += parent_dna[i].ToString();
                            break;
                    }

                else
                    dna += parent_dna[i].ToString(); //If the universe doesn't want a mutation, then just keep the parent's nitrogenous base
            }

            setCellProperties();
        }



        private void setCellProperties() {  //It sets the cell's properties once the DNA is already built
            setGenome();  //get the genes within the DNA string
            transcribe();   //convert the DNA into mRNA's
            translate(); //convert mRNA's into proteins
        }



        /* The next method ("setGenome()") will anlize the DNA string and will get all the genes within it and add them to the "genome List".
         * 
         * To understand the algorithm, you should keep in mind the very simplified definition of "gene" I'll use:
         *    -Every gene will start with the codon "TAC" and will end with one of three ending codons ("ATT", "ATC", or "ACT")
         *    -(I know it's much more complex in real organisms, but I can't take the real model becuase it is... Too complex)
         * 
         * This is how the algorithm works:
         *    -There is a boolean variable called "itsBeingTransposed" wich defines wheather or not the part of the DNA string is
         *    being transposed into a "gene" string (I'll explain this string soon)
         *    -While the bool remains false, nothing is going to occur, until it reads a start codon "TAC"
         *    -The start codon sets the bool true
         *    -Once the bool is true, every codon will be transposed to the "gene" until it finds a stop codon
         *    -(The "gene" string is the string wich will store all the codons of a gene and is the one that is going to be added to the "genome" List.
         *    Each time the program reachs a stop codon, the "gene" string is reseted (gene = "";) )
         *    -Every gene will be added to the "genome" List until the the program reachs the last DNA string's codon
         */

        private void setGenome() {  //It creates the List wich contain all the genes within the DNA string
            bool itsBeingTransposed = false; //I've already explained it :)
            string gene = ""; //This variable will contain different genes during the reading process

            int necessaryForStop = 0;      // 
            switch (dna.Length % 3) {      //This is needed so the program know where is index of the last codon
                case 0:                    //
                    necessaryForStop = 2;  //If the DNA string ends with  ... ATG / CGT, so we need the program stops in "C", so "last intdex"-2
                    break;                 //
                case 1:                    //
                    necessaryForStop = 3;  //If the DNA string ends with  ... TAA / GTA / C, so we need the program stops in "G", so "last intdex"-3
                    break;                 //
                case 2:                    //
                    necessaryForStop = 4;  //If the DNA string ends with  ... TCG / ATG / CG, so we need the program stops in "A", so "last intdex"-4
                    break;                 //
            }
            
            for (int i = 0; i < dna.Length - necessaryForStop; i +=3) { //It loops throught the DNA string
                string dnaCodon = dna[i].ToString() + dna[i+1].ToString() + dna[i+2]; //It gets the codon will be analized. Ex: "ATC", "TAC", "CGA", etc

                if (!itsBeingTransposed && dnaCodon == "TAC") //If it was not already transposing and it finds a "TAC" codon: 
                    itsBeingTransposed = true; //Then start transposing

                if (itsBeingTransposed && (dnaCodon == "ATT" || dnaCodon == "ATC" || dnaCodon == "ACT")) { //If it was transposing and finds a stop codon:
                    itsBeingTransposed = false; //Then stop transposing,

                    genome.Add(gene); //Add the "gene" string,
                    gene = ""; //And, after that, reset the "gene" string
                }

                if (itsBeingTransposed) //If it's transposing:
                    gene += dnaCodon;  //Then add the codon to the "gene" string

            }
        }




        private void transcribe() {  //This method will get the genes and convert them into mRNA's
            string[] genome = this.genome.ToArray();  //This is the array version of the "genome" List that is going to be used
            mrna = new string[genome.Length];

            for (int ii = 0; ii < genome.Length; ii++) { //It will loop throught the "genome" array
                string gene = genome[ii]; //It will store the gene that is going to be analized
                string strand = ""; //It will store the mRNA

                for (int i = 0; i < gene.Length; i++) { //It will loop throught a string within the "genome" array
                    switch (gene[i]) { 
                        case 'A':
                            strand += "U";
                            break;
                        case 'T':
                            strand += "A";
                            break;
                        case 'C':
                            strand += "G";
                            break;
                        case 'G':
                            strand += "C";
                            break;
                    }
                }

                mrna[ii] = strand;
            }
        }



        private bool The_Universe_Wants_a_Mutation_Here() { 
            return 0 == random.Next( (int)(100/mutationRate) );  //It basically work by the mutation rate's percentage
        }



        private string[] aCodon =    {"UUU", "UUC", "UUA", "UUG", "UCU", "UCC", "UCA", "UCG", "UAU", "UAC", "UAA", "UAG", "UGU", "UGC", "UGA", "UGG", 
                                      "CUU", "CUC", "CUA", "CUG", "CCU", "CCC", "CCA", "CCG", "CAU", "CAC", "CAA", "CAG", "CGU", "CGC", "CGA", "CGG", 
                                      "AUU", "AUC", "AUA", "AUG", "ACU", "ACC", "ACA", "ACG", "AAU", "AAC", "AAA", "AAG", "AGU", "AGC", "AGA", "AGG", 
                                      "GUU", "GUC", "GUA", "GUG", "GCU", "GCC", "GCA", "GCG", "GAU", "GAC", "GAA", "GAG", "GGU", "GGC", "GGA", "GGG"};
                                                                                                                                                               //These arrays are related,
        private string[] aminoAcid = {"Phe", "Phe", "Leu", "Leu", "Ser", "Ser", "Ser", "Ser", "Tyr", "Tyr", "stp", "stp", "Cys", "Cys", "stp", "Trp",          //each codon relates to an amino acid
                                      "Leu", "Leu", "Leu", "Leu", "Pro", "Pro", "Pro", "Pro", "His", "His", "Gln", "Gln", "Arg", "Arg", "Arg", "Arg", 
                                      "Ile", "Ile", "Ile", "Met", "Thr", "Thr", "Thr", "Thr", "Asn", "Asn", "Lys", "Lys", "Ser", "Ser", "Arg", "Arg", 
                                      "Val", "Val", "Val", "Val", "Ala", "Ala", "Ala", "Ala", "Asp", "Asp", "Glu", "Glu", "Gly", "Gly", "Gly", "Gly"};

        private void translate() { //It's very confusing, I know. But, basically, what it does is to get the mRNA's and convert them into proteins
            protein = new string[mrna.Length];

            for (int iii = 0; iii < mrna.Length; iii++) {
                string strand = mrna[iii];
                string aminoAcidChain = "";

                for (int ii = 0; ii < strand.Length-2; ii += 3) {
                    string codon = strand[ii].ToString() + strand[ii+1].ToString() + strand[ii+2].ToString();

                    for (int i = 0; i < aCodon.Length; i++) {
                        if (aCodon[i] == codon)
                            aminoAcidChain += aminoAcid[i];
                    }

                }

                protein[iii] = aminoAcidChain;
            }
        }


        //accessors and mutators:

        public string[] Genome { get { return genome.ToArray(); } }
        public string[] Protein { get { return protein; } }
        public string[] mRNA { get { return mrna; } }
        public string DNA { get { return dna; } }

        public int Initial_DNA_Length { set { initial_dnaLength = value; } }
        public double MutationRate { set { mutationRate = value; } }

    }
}
