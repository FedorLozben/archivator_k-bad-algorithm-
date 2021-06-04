using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace archeive_maker_badas
{
    class tr
    {
        public tr r;
        public tr l;
        public char data;
        public int w;
    }
    class arch
    {
        private int[] code_binary = new int[5000];
        private int[] code_binary_max = new int[5000];
        private int readen;
        tr ttt;
        private void generate_code(int code, int max, tr t)
        {
            if (t.l != null)
            {
                generate_code(code * 2, max + 1, t.l);
            }
            if (t.r != null)
            {
                generate_code(code * 2 + 1, max + 1, t.r);
            }
            if ((t.r == null) && (t.l == null))
            {
                code_binary[(int)(t.data)] = code;
                code_binary_max[(int)(t.data)] = max;
            }
        }
        private char get_letter(int number, int max, tr trr)
        {
            tr goes = trr;
            int dig = 1;

            for (int go = 0; go < max; go++)
            {
                dig *= 2;
            }
            dig /= 2;
            while (max > 0)
            {
                if (number >= dig)
                {//1
                    number -= dig;
                    if (goes.r == null) { return '\0'; }
                    goes = goes.r;
                }
                else
                {//0
                    if (goes.l == null) { return '\0'; }
                    goes = goes.l;
                }
                dig /= 2;
                max--;
                if ((goes.l == null) && (goes.r == null) && (max == 0)) { return goes.data; }
            }
            return '\0';
        }
        public void decode_tree(tr trr, StreamReader sr)
        {
            if (readen != 3)
            {
                readen = (int)(sr.Read());
                trr.l = null;
                trr.l = null;
                if (readen == 1)
                {
                    trr.l = new tr();
                    decode_tree(trr.l, sr);
                }
                if (readen == 2)
                {
                    trr.r = new tr();
                    decode_tree(trr.r, sr);
                }
                if (readen == 3)
                {
                }
                else
                {
                    if (readen > 2)
                    {
                        trr.data = (char)(readen);
                        readen = (int)(sr.Read());
                    }
                }
            }
        }
        public void decode()
        {
            tr trr = new tr();
            using (StreamReader sr = new StreamReader("C:/Users/lozbenfe/source/repos/archivatorkafmana/archivatorkafmana/small.txt"))
            {
                using (StreamWriter sw = new StreamWriter("C:/Users/lozbenfe/source/repos/archivatorkafmana/archivatorkafmana/large_new.txt"))
                {
                    readen = 11;
                    decode_tree(trr, sr);

                    int runner = 0, max = 0, temp = 0, temp_max = 0, dig = 0, d;
                    int temporary_runner = 0, temporary_max = 0;
                    char goes_to_file;
                    char[] temporary_storge = new char[3];
                    temporary_storge[0] = '\0';
                    int temporary_temp = 0, temporary_temp_max = 0;
                    int[] t = new int[2];
                    t[0] = -1;
                    t[1] = -1;
                    while (sr.Peek() != -1)
                    {
                        d = (int)(sr.Read());
                        t[0] = t[1];
                        t[1] = d;
                        if (t[0] != -1)
                        {
                            if (sr.Peek() != -1)
                            {
                                max += 8;
                                runner *= 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2;

                                runner += t[0];

                                dig = 1;
                                for (int go = 0; go < max; go++) { dig *= 2; }
                                dig /= 2;

                                while (max > 0)
                                {
                                    if (runner >= dig)
                                    {//1
                                        runner -= dig;
                                        temp *= 2;
                                        temp += 1;
                                    }
                                    else
                                    {//0
                                        temp *= 2;
                                    }
                                    temp_max++;
                                    dig /= 2;
                                    goes_to_file = get_letter(temp, temp_max, trr);
                                    if (goes_to_file != '\0')
                                    {
                                        sw.Write(goes_to_file);
                                        temp_max = 0;
                                        temp = 0;
                                    }
                                    max--;
                                }
                            }
                            else
                            {
                                max += d;
                                runner *= 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2;

                                runner += t[0];

                                dig = 1;
                                for (int go = 0; go < max; go++) { dig *= 2; }
                                dig /= 2;

                                while (max > 0)
                                {
                                    if (runner >= dig)
                                    {//1
                                        runner -= dig;
                                        temp *= 2;
                                        temp += 1;
                                    }
                                    else
                                    {//0
                                        temp *= 2;
                                    }
                                    temp_max++;
                                    dig /= 2;
                                    goes_to_file = get_letter(temp, temp_max, trr);
                                    if (goes_to_file != '\0')
                                    {
                                        sw.Write(goes_to_file);
                                        temp_max = 0;
                                        temp = 0;
                                    }
                                    max--;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void fire_tree(tr trr, StreamWriter sw)
        {
            if (trr.l != null)
            {
                sw.Write((char)(1));
                fire_tree(trr.l, sw);
            }
            if (trr.r != null)
            {
                sw.Write((char)(2));
                fire_tree(trr.r, sw);
            }
            if ((trr.l == null) && (trr.r == null)) { sw.Write(trr.data); }
        }
        private void fire(tr trr)
        {
            ttt = trr;
            generate_code(0, 0, trr);
            int letter;
            using (StreamReader sr = new StreamReader("C:/Users/lozbenfe/source/repos/archivatorkafmana/archivatorkafmana/large.txt"))
            {
                using (StreamWriter sw = new StreamWriter("C:/Users/lozbenfe/source/repos/archivatorkafmana/archivatorkafmana/small.txt"))
                {
                    using (StreamWriter swn = new StreamWriter("C:/Users/lozbenfe/source/repos/archivatorkafmana/archivatorkafmana/small_debug.txt"))
                    {

                        fire_tree(trr, sw);
                        fire_tree(trr, swn);

                        sw.Write((char)(3));
                        swn.Write(3);

                        int runner = 0, max = 0, dig = 1;
                        while (sr.Peek() > 0)
                        {
                            letter = (int)(sr.Read());
                            for (int go = 0; go < code_binary_max[letter]; go++)
                            {
                                runner *= 2;
                                max++;
                                dig *= 2;
                            }
                            runner += code_binary[letter];
                            if (max >= 8)
                            {
                                dig = 1;
                                for (int go = 0; go < max; go++)
                                {
                                    dig *= 2;
                                }
                                dig /= 2;
                                int temp = 0;
                                for (int go = 0; go < 8; go++)
                                {
                                    if (runner >= dig)
                                    {//1
                                        temp *= 2;
                                        temp += 1;
                                        runner -= dig;
                                    }
                                    else
                                    {//0
                                        temp *= 2;
                                    }
                                    dig /= 2;
                                    max--;
                                }
                                sw.Write((char)(temp));
                                swn.Write(temp + " ");
                            }

                        }

                        swn.Write(runner + " ");
                        swn.Write(max + " ");
                        sw.Write((char)(runner));
                        sw.Write((char)(max));
                    }
                }
            }
        }
        public void archeive()
        {
            int[] w = new int[500];
            int[] c = new int[500];
            for (int go = 0; go < 500; go++) { w[go] = 0; c[go] = go; }
            using (StreamReader sr = new StreamReader("C:/Users/lozbenfe/source/repos/archivatorkafmana/archivatorkafmana/large.txt"))
            {
                while (sr.Peek() > 1)
                {
                    w[(int)(sr.Read())]++;
                }
            }

            bool act = true;
            int dop;
            while (act == true)
            {
                act = false;
                for (int i = 0; i < 500 - 1; i++)
                {
                    int j = i + 1;
                    if ((((w[j] < w[i]) || (w[i] == 0))) && (w[j] != 0))
                    {
                        act = true;

                        dop = w[i];
                        w[i] = w[j];
                        w[j] = dop;

                        dop = c[i];
                        c[i] = c[j];
                        c[j] = dop;
                    }
                }


            }

            int N = 0;
            while (w[N] != 0) { N++; }

            tr[] mas = new tr[N];
            for (int go = 0; go < N; go++)
            {
                mas[go] = new tr();
                mas[go].w = w[go];
                mas[go].data = (char)(c[go]);
                mas[go].r = null;
                mas[go].l = null;
            }

            while (N > 1)
            {
                tr mmm = new tr();
                mmm.l = mas[0];
                mmm.r = mas[1];
                mmm.w = mas[0].w + mas[1].w;
                mmm.data = '_';
                int go = 0;
                while (go < N - 1)
                {
                    mas[go] = mas[go + 1];
                    go++;
                }
                N--;
                go = 0;
                while ((go < N - 1) && ((mas[go].w > mmm.w) || (mas[go + 1].w < mmm.w)))
                {
                    mas[go] = mas[go + 1];
                    go++;
                }
                mas[go] = mmm;
            }

            fire(mas[0]);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            arch ar = new arch();
            ar.archeive();
            Console.Write("MESSAGE HAS BEEN CODED:");
            int a = Console.Read();
            ar.decode();
            Console.Write("MESSAGE HAS BEEN DECODED:");
            while (true) {; }
        }
    }
}
