﻿/*
  Project Kraken: SPARK for Microsoft SharePoint 2010
  Copyright (C) 2003-2011 Thomas Carpe. <http://www.ThomasCarpe.com/>
  Maintained by: <http://www.LiquidMercurySolutions.com/>

  This file is part of SPARK: SharePoint Application Resource Kit.
  SPARK projects are distributed via CodePlex: <http://www.codeplex.com/spark/>

  You may use this code for commercial purposes and derivative works, 
  provided that you maintain all copyright notices.

  SPARK is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version. You should have received a copy of
  the GNU General Public License along with SPARK.  If not, see
  <http://www.gnu.org/licenses/>.

  SPARK is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.
  
  We worked hard on all SPARK code, and we don't make any profit from
  sharing it with the world. Please do us a favor amd give us credit
  where credit is due, by leaving this notice unchanged. We all stand
  on the backs of giants. Wherever we have used someone else's code or
  blog article as the basis of our work, we have provided references
  to our source.
*/

namespace Kraken.SharePoint.Logging.Unsafe {

  using System;
  using System.Runtime.InteropServices;
  
  // Wrapper class to manually marshal data to unmanaged memory. 
  // This lets us avoid using the "unsafe" keyword 
  struct DataDescriptorWrapper : IDisposable {
    public IntPtr Ptr;
    // Pointer to data 
    public DataDescriptorWrapper(EVENT_DATA_DESCRIPTOR descriptor) {
      Ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(EVENT_DATA_DESCRIPTOR)));
      Marshal.StructureToPtr(descriptor, Ptr, false);
    }
    void IDisposable.Dispose() {
      Marshal.FreeHGlobal(Ptr);
    }
  }

}
