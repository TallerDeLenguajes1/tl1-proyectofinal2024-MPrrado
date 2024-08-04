using System.Collections.Generic;
using System.Net.NetworkInformation;
namespace Text
{
    public static class Textos
    {
        private const string tituloPrincipal = @"
      __          __                         _____ _                      
      \ \        / /                        / ____| |                     
       \ \  /\  / ___  _ __ _ __ ___  ___  | (___ | |__   __ _ _ __ _ __  
        \ \/  \/ / _ \| '__| '_ ` _ \/ __|  \___ \| '_ \ / _` | '__| '_ \ 
         \  /\  | (_) | |  | | | | | \__ \  ____) | | | | (_| | |  | |_) |
          \/  \/ \___/|_|  |_| |_| |_|___/ |_____/|_| |_|\__,_|_|  | .__/ 
                                                                   | |    
                                                                   |_|    ";

        private const string generandoPersonajes= @"
   _____________   ____________  ___    _   ______  ____     ____  __________  _____ ____  _   _____       _____________
  / ____/ ____/ | / / ____/ __ \/   |  / | / / __ \/ __ \   / __ \/ ____/ __ \/ ___// __ \/ | / /   |     / / ____/ ___/
 / / __/ __/ /  |/ / __/ / /_/ / /| | /  |/ / / / / / / /  / /_/ / __/ / /_/ /\__ \/ / / /  |/ / /| |__  / / __/  \__ \ 
/ /_/ / /___/ /|  / /___/ _, _/ ___ |/ /|  / /_/ / /_/ /  / ____/ /___/ _, _/___/ / /_/ / /|  / ___ / /_/ / /___ ___/ / 
\____/_____/_/ |_/_____/_/ |_/_/  |_/_/ |_/_____/\____/  /_/   /_____/_/ |_|/____/\____/_/ |_/_/  |_\____/_____//____/  
                                                                                                                        ";
        public static string TituloPrincipal => tituloPrincipal;

        public static string GenerandoPersonajes => generandoPersonajes;

        public static void CentrarTexto()
        {
            Console.MoveBufferArea(0,0,Console.BufferWidth,Console.BufferHeight,40,0);
        }
    }

}