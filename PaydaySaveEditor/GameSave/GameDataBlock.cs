﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PD2.GameSave
{
	public class GameDataBlock : DataBlock
	{
		private Dictionary<Object, Object> dictionary;
        public Dictionary<Object, Object> Dictionary { get { return this.dictionary; } }

		public GameDataBlock(BinaryReader br) : base(br)
		{
			BinaryReader dataBr = new BinaryReader(new MemoryStream(data));

			// The game data block could potentially not be
			// just a dictionary, but we assume that it must
			// be as I haven't seen any saves that don't con-
			// tain a dictionary as a root node
			this.dictionary = (Dictionary<Object, Object>) GameData.DeserializeData(dataBr);
		}

		new public byte[] ToArray()
		{
			this.data = GameData.SerializeData(this.dictionary);
			return base.ToArray();
		}
	}
}